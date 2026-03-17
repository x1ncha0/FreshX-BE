using FluentValidation;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Application.Interfaces.Payments;
using FreshX.Application.Interfaces.ServiceType;
using FreshX.Application.Interfaces.Services;
using FreshX.Application.Mapping;
using FreshX.Application.Services;
using FreshX.Application.Validation;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using FreshX.Infrastructure.Repositories;
using FreshX.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=FreshX;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FreshXDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDataProtection();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FreshXDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(DataAnnotationsBridgeValidator<>).Assembly);

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDrugTypeRepository, DrugTypeRepository>();
builder.Services.AddScoped<IDrugTypeService, DrugTypeService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IExamineRepository, ExamineRepository>();
builder.Services.AddScoped<IExamineService, ExamineService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ILabResultRepository, LabResultRepository>();
builder.Services.AddScoped<ILabResultService, LabResultService>();
builder.Services.AddScoped<IMedicalServiceRequestRepository, MedicalServiceRequestRepository>();
builder.Services.AddScoped<IMedicalServiceRequestService, MedicalServiceRequestService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
builder.Services.AddScoped<IPrescriptionDetailService, PrescriptionDetailService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IBillingRepository, BillingRepository>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IPdfRepository, PdfRepository>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
