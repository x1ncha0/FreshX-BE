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
using Microsoft.Extensions.DependencyInjection;

namespace FreshX.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, typeof(AutoMapperProfile));
        services.AddValidatorsFromAssembly(typeof(DataAnnotationsBridgeValidator<>).Assembly);

        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IDrugTypeService, DrugTypeService>();
        services.AddScoped<IExamineService, ExamineService>();
        services.AddScoped<ILabResultService, LabResultService>();
        services.AddScoped<IMedicalServiceRequestService, MedicalServiceRequestService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IPrescriptionDetailService, PrescriptionDetailService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<IBillingService, BillingService>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IServiceTypeService, ServiceTypeService>();

        return services;
    }
}
