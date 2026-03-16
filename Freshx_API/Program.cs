using AutoMapper;
using Azure.Identity;
using DotNetEnv;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces.UserAccount;
using Freshx_API.Mappers;
using Freshx_API.Models;
using Freshx_API.Repository;
using Freshx_API.Repository.Address;
using Freshx_API.Repository.Auth.AccountRepositories;
using Freshx_API.Repository.Auth.RoleRepositories;
using Freshx_API.Repository.Auth.TokenRepositories;
using Freshx_API.Repository.Drugs;
using Freshx_API.Repository.UserAccount;
using Freshx_API.Services;
using Freshx_API.Services.SignalR;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services.Drugs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Freshx_API.Interfaces.Payments;
using Freshx_API.Repository.Payments;
using Freshx_API.Repository.Payments;
using Freshx_API.Interfaces.IReception;
using Freshx_API.Repository.LabResults;
using Freshx_API.Interfaces.Services;
using Org.BouncyCastle.Math.EC.Multiplier;
using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Interfaces.ServiceType;
using System.Net;
using System.Reflection;
using Hangfire;
using Hangfire.SqlServer;
using Freshx_API.Services.HangfireService;
// Tải biến môi trường từ tệp .env
Env.Load();
var builder = WebApplication.CreateBuilder(args);


Console.OutputEncoding = System.Text.Encoding.UTF8;
// hiển thị phiên bản
static DateTime GetBuildDate()
{
    var filePath = Assembly.GetExecutingAssembly().Location;
    return File.GetLastWriteTime(filePath); // Lấy thời gian file .dll được build
}

var buildDate = GetBuildDate();

// Hiển thị thông tin
Console.WriteLine($"Thời gian phát hành: {buildDate:yyyy-MM-dd HH:mm:ss}");

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API DATN", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
            });
});
//Tránh lỗi StackOverflowException khi serialize
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Đọc password và salt từ biến môi trườn g
string password = Environment.GetEnvironmentVariable("ENCRYPTION_PASSWORD")
?? builder.Configuration["EncryptionSettings:Password"]
?? "DefaultPassword";
string saltString = Environment.GetEnvironmentVariable("ENCRYPTION_SALT")
?? builder.Configuration["EncryptionSettings:Salt"]
?? "DefaultSalt";
//kết thúc biến môi trường

// Kiểm tra nếu không lấy được biến môi trường và dừng quá trình build
if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(saltString))
{
    Console.WriteLine("Lỗi: Không thể lấy được ENCRYPTION_PASSWORD hoặc ENCRYPTION_SALT từ biến môi trường.");
    throw new InvalidOperationException("Dừng quá trình build vì thiếu biến môi trường.");
}

byte[] salt = Encoding.UTF8.GetBytes(saltString);


builder.Configuration.AddConfiguration(
    new ConfigurationBuilder()
        .Add(new EncryptedConfigurationSource(password, salt))
        .Build());

var connectionString = builder.Configuration["ConnectionStrings:DBFreshx"];
var jwtKey = builder.Configuration["Jwt:Key"];
var blobConnectionString = builder.Configuration["AzureBlobStorage:ConnectionString"];
var containerName = builder.Configuration["AzureBlobStorage:ContainerName"];
Console.WriteLine("jkds" + builder.Configuration["FileSettings:DevicePath"]);
// Add services to the container.
builder.Services.AddDbContext<FreshxDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});
// Add hangfire service
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions  // Sửa chỗ này
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();
//configure Identity Service
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    // Thiet lap khoa tài kho?n
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Thoi gian khóa
    options.Lockout.MaxFailedAccessAttempts = 5; // So lan sai mat khau toi da
    options.Lockout.AllowedForNewUsers = true; // Cho phép khóa tài khoan moi
})
.AddEntityFrameworkStores<FreshxDBContext>()
.AddDefaultTokenProviders();
builder.Services.AddIdentityCore<AppUser>()
    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
//
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new ValidationError
            {
                Field = e.Key,
                Message = e.Value?.Errors.First().ErrorMessage ?? "Dữ liệu đầu vào không hợp lệ"
            })
            .ToList();

        var response = new ApiResponse<List<ValidationError>>
        {
            Status = false,
            Path = context.HttpContext.Request.Path,
            Message ="Dữ liệu đầu vào không hợp lệ",
            StatusCode = StatusCodes.Status400BadRequest,
            Data = errors,
            Timestamp = DateTime.UtcNow
        };

        /*return new BadRequestObjectResult(response);*/
        return new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    };
}); ;
// Cấu hình Kestrel để lắng nghe trên Tailscale IP
builder.WebHost.ConfigureKestrel((context, options) =>
{
    // Kiểm tra nếu đang ở môi trường Production
    if (context.HostingEnvironment.IsProduction())
    {
        // Chỉ lắng nghe tất cả các IP khi ở môi trường Production
        options.Listen(System.Net.IPAddress.Any, 5000);
    }
    else
    {
        
    }
    //options.Listen(System.Net.IPAddress.Any,5000); // Lắng nghe trên tất cả các IP
});
// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
        RoleClaimType = ClaimTypes.Role
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var tokenValidUntil = context.SecurityToken.ValidTo;
            if (DateTime.UtcNow > tokenValidUntil)
            {
                context.Fail("Token has expired");
            }

            var userClaims = context.Principal.Claims;
            if (!userClaims.Any())
            {
                context.Fail("Token contains no claims");
            }

            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
        {
            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }
            context.NoResult();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            string errorMessage = context.Exception switch
            {
                SecurityTokenExpiredException _ => "Token has expired",
                SecurityTokenInvalidSignatureException _ => "Invalid token signature",
                SecurityTokenInvalidLifetimeException _ => "Token lifetime is invalid",
                SecurityTokenNotYetValidException _ => "Token is not valid yet",
                SecurityTokenMalformedException _ => "Malformed token",
                _ => "Invalid token"
            };

            var response = new ApiResponse<object>
            {
                Status = false,
                Path = context.Request.Path,
                Message = errorMessage,
                StatusCode = StatusCodes.Status401Unauthorized,
                Data = null,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsJsonAsync(response);  // Added return
        },

        OnChallenge = context =>
        {
            context.HandleResponse();
            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            string message = string.IsNullOrEmpty(context.Error)
                ? "Unauthorized access"
                : context.Error switch
                {
                    "invalid_token" => "The token is invalid",
                    "invalid_grant" => "The authorization grant is invalid",
                    _ => $"Authorization failed: {context.Error}"
                };

            var response = new ApiResponse<object>
            {
                Status = false,
                Path = context.Request.Path,
                Message = message,
                StatusCode = StatusCodes.Status401Unauthorized,
                Data = null,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsJsonAsync(response);  // Added return
        },

        OnForbidden = context =>
        {
            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>
            {
                Status = false,
                Path = context.Request.Path,
                Message = "You don't have permission to access this resource",
                StatusCode = StatusCodes.Status403Forbidden,
                Data = null,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsJsonAsync(response);  // Added return
        }
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm cấu hình AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    // Thêm các profile của bạn ở đây
    // mc.AddProfile(new YourAutoMapperProfile());
});
// Custom route lowercase
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true; // Tùy chọn: lowercase cả query string
});
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// tiếp nhận
builder.Services.AddScoped<IReceptionRepository, ReceptionRepository>();
builder.Services.AddScoped<IReceptionService, ReceptionService>();

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<NumberGeneratorService>();
builder.Services.AddScoped<IFixDoctorRepository, FixDoctorRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ITechnicianRepository,TechnicianRepository>();
builder.Services.AddScoped<IUserAccountManagementRepository,UserAccountManagementRepository>();
builder.Services.AddScoped<IFixDepartmentTypeRepository, FixDepartmentTypeRepository>();
builder.Services.AddScoped<IFixDepartmentRepository, FixDepartmentRepositiory>();
builder.Services.AddScoped<IOnlineAppointmentRepository,OnlineAppointmentRepository>();
// Thêm AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IDrugTypeRepository, DrugTypeRepository>();
builder.Services.AddScoped<IDrugTypeService, DrugTypeService>();

builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddScoped<PharmacyService>();

// medical service - dịch vụ y tế
builder.Services.AddScoped<IMedicalServiceRequestRepository, MedicalServiceRequestRepository>();
builder.Services.AddScoped<IMedicalServiceRequestService, MedicalServiceRequestService>();

// Đăng ký Repository và Service với Dependency Injection
builder.Services.AddScoped<IDepartmentTypeRepository, DepartmentTypeRepository>();
builder.Services.AddScoped<DepartmentTypeService>();


builder.Services.AddScoped<IDrugTypeRepository, DrugTypeRepository>();
builder.Services.AddScoped<IDrugTypeService, DrugTypeService>();

builder.Services.AddScoped<IPharmacyRepository,PharmacyRepository>();
builder.Services.AddScoped<PharmacyService>();


// Đăng ký Repository và Service với Doctor Injection
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<DoctorService>();

// Đăng ký Repository và Service với Dependency Injection
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<DepartmentService>();


// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<IInventoryTypeRepository, InventoryTypeRepository>();
builder.Services.AddScoped<InventoryTypeService>();

// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddScoped<PharmacyService>();



// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<IServiceGroupRepository, ServiceGroupRepository>();
builder.Services.AddScoped<ServiceGroupService>();


// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();
builder.Services.AddScoped<ServiceCatalogService>();


// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
builder.Services.AddScoped<UnitOfMeasureService>();

// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<SupplierService>();


// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<CountryService>();

// Đăng ký Repository và Service với InventoryType Injection
builder.Services.AddScoped<IDrugCatalogRepository, DrugCatalogRepository>();
builder.Services.AddScoped<DrugCatalogService>();

//Dăng kí Reponsitory và service cho địa chỉ
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
// Đăng ký PdfRepository và PdfService
builder.Services.AddScoped<IPdfRepository, PdfRepository>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<ChatService>();

//đăng kí service
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();


//Đăng ký Repository và service cho payments
builder.Services.AddScoped<IBillingRepository, BillingRepository>();
builder.Services.AddScoped<IBillingService, BillingService>();

// Đăng kí labReSult
builder.Services.AddScoped<ILabResultRepository, LabResultRepository>();
builder.Services.AddScoped<ILabResultService, LabResultService>();

//Đăng kí Prescription - toa thuốc - toa thuốc chi tiết
builder.Services.AddScoped<IPrescriptionService,PrescriptionService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
builder.Services.AddScoped<IPrescriptionDetailService, PrescriptionDetailService>();

//Đăng kí loại dịch vụ servicetype
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();

// khám bệnh
builder.Services.AddScoped<IExamineRepository, ExamineRepository>();
builder.Services.AddScoped<IExamineService, ExamineService>();
// đăng kí repositorycheck dùng để check trùng lặp
builder.Services.AddScoped<RepositoryCheck>();

// Thêm DefaultAzureCredential
builder.Services.AddSingleton<DefaultAzureCredential>();

        // Thêm DefaultAzureCredential
        builder.Services.AddSingleton<DefaultAzureCredential>();
        // Đăng ký IHttpContextAccessor để có thể truy cập HttpContext
        builder.Services.AddHttpContextAccessor();
// Thêm DefaultAzureCredential
builder.Services.AddSingleton<DefaultAzureCredential>();

// Đăng ký IHttpContextAccessor để có thể truy cập HttpContext
builder.Services.AddHttpContextAccessor();
// Cấu hình Kestrel để lắng nghe trên tất cả các địa chỉ IP
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.Listen(IPAddress.Any, 7075); // Hoặc địa chỉ IP của thiết bị
//});


var app = builder.Build();


// 5. Configure Hangfire Dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "FreshX",
    DisplayStorageConnectionString = false
});
app.ConfigureAppointmentJobs();
// Cấu hình CORS để cho phép truy cập từ mọi nguồn
// Enable CORS
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowAnyMethod();
           
});

app.MapHub<ChatHub>("/chathub").RequireCors(policy =>
{
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .SetIsOriginAllowed(origin => true) // Tùy chọn: Chấp nhận tất cả origin
          .AllowCredentials();                // Cho phép tín hiệu sử dụng cookie
}); ;

app.MapHub<NotificationHub>("/notificationHub").RequireCors(policy =>
{
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .SetIsOriginAllowed(origin => true) // Tùy chọn: Chấp nhận tất cả origin
          .AllowCredentials();                // Cho phép tín hiệu sử dụng cookie
});
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//chạy swagger trên puplig
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();
//xac thuc truoc khi phan quyen
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
