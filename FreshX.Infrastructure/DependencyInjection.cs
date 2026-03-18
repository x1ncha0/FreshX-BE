using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Application.Interfaces.Payments;
using FreshX.Application.Interfaces.ServiceType;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using FreshX.Infrastructure.Repositories;
using FreshX.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            .AddEntityFrameworkStores<FreshXDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDrugTypeRepository, DrugTypeRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IExamineRepository, ExamineRepository>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILabResultRepository, LabResultRepository>();
        services.AddScoped<IMedicalServiceRequestRepository, MedicalServiceRequestRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IBillingRepository, BillingRepository>();
        services.AddScoped<IPdfRepository, PdfRepository>();
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();

        return services;
    }
}
