using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Application.Interfaces.Payments;
using FreshX.Application.Interfaces.ServiceType;
using FreshX.Application.Interfaces.UserAccount;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using FreshX.Infrastructure.Repositories;
using FreshX.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace FreshX.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? "Server=(localdb)\\mssqllocaldb;Database=FreshX;Trusted_Connection=True;TrustServerCertificate=True;";

        services.AddDbContext<FreshXDbContext>(options => options.UseSqlServer(connectionString));
        services.AddHttpContextAccessor();

        services
            .AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddEntityFrameworkStores<FreshXDbContext>()
            .AddDefaultTokenProviders();

        var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key configuration is required.");
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuer = !string.IsNullOrWhiteSpace(issuer),
                    ValidIssuer = issuer,
                    ValidateAudience = !string.IsNullOrWhiteSpace(audience),
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDrugCatalogRepository, DrugCatalogRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDrugTypeRepository, DrugTypeRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IExamineRepository, ExamineRepository>();
        services.AddScoped<IFixDepartmentTypeRepository, FixDepartmentTypeRepository>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IInventoryTypeRepository, InventoryTypeRepository>();
        services.AddScoped<ILabResultRepository, LabResultRepository>();
        services.AddScoped<IMedicalServiceRequestRepository, MedicalServiceRequestRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IBillingRepository, BillingRepository>();
        services.AddScoped<IPdfRepository, PdfRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();
        services.AddScoped<IServiceGroupRepository, ServiceGroupRepository>();
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ITechnicianRepository, TechnicianRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
        services.AddScoped<IUserAccountManagementRepository, UserAccountManagementRepository>();
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();

        return services;
    }
}
