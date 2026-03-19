using FluentValidation;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Application.Interfaces.Payments;
using FreshX.Application.Interfaces.ServiceType;
using FreshX.Application.Interfaces.Services;
using FreshX.Application.Interfaces.UserAccount;
using FreshX.Application.Interfaces.UserAccountManagement;
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
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDrugCatalogService, DrugCatalogService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IDrugTypeService, DrugTypeService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IExamineService, ExamineService>();
        services.AddScoped<IFixDepartmentTypeService, FixDepartmentTypeService>();
        services.AddScoped<IInventoryTypeService, InventoryTypeService>();
        services.AddScoped<ILabResultService, LabResultService>();
        services.AddScoped<IMedicalServiceRequestService, MedicalServiceRequestService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IPharmacyService, PharmacyService>();
        services.AddScoped<IPrescriptionDetailService, PrescriptionDetailService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<IBillingService, BillingService>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IServiceCatalogService, ServiceCatalogService>();
        services.AddScoped<IServiceGroupService, ServiceGroupService>();
        services.AddScoped<IServiceTypeService, ServiceTypeService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ITechnicianService, TechnicianService>();
        services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
        services.AddScoped<IUserAccountManagementService, UserAccountManagementService>();
        services.AddScoped<IUserAccountService, UserAccountService>();

        return services;
    }
}
