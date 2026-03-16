using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Dtos.Auth.Account;
using Freshx_API.Dtos.Auth.Role;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Dtos.Doctor;
using Freshx_API.Dtos.DepartmentDtos;
using Freshx_API.Dtos.Drugs;
using Freshx_API.Models;
using Microsoft.AspNetCore.Identity;
using Freshx_API.Dtos.InventoryType;
using Freshx_API.Dtos.Pharmacy;
using Freshx_API.Dtos.ServiceGroup;
using Freshx_API.Dtos.ServiceCatalog;
using Freshx_API.Dtos.UnitOfMeasure;
using Freshx_API.Dtos.Supplier;
using System.Diagnostics.Metrics;
using Freshx_API.Dtos.Country;
using Freshx_API.Dtos.DrugCatalog;
using Freshx_API.Dtos.Payments;
using Freshx_API.Dtos.UserAccount;
using Freshx_API.Dtos.Patient;
using Freshx_API.Dtos.Employee;
using Freshx_API.Dtos.UserAccountManagement;
using Freshx_API.Dtos.ExamineDtos;
using Freshx_API.Dtos.Prescription;
using Freshx_API.Dtos.TmplPrescription;
namespace Freshx_API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Savefile, FileDto>().ReverseMap();
            CreateMap<IdentityRole, RoleResponse>();
            CreateMap<AppUser, RegisterResponse>();
            CreateMap<DrugType, DrugTypeDto>().ReverseMap();
            CreateMap<DrugTypeCreateDto, DrugType>();
            CreateMap<DrugTypeUpdateDto, DrugType>();

            // Mapping từ Model sang DTO
            CreateMap<DepartmentType, DepartmentTypeDto>();
            CreateMap<DepartmentType, DepartmentTypeDetailDto>();

            // Mapping từ DTO sang Model khi tạo hoặc cập nhật
            CreateMap<DepartmentTypeCreateUpdateDto, DepartmentType>();

            // Doctor Mappings
            CreateMap<Doctor, DoctorDto>()
             // Chỉ map những trường có tên khác nhau
             .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name))
             .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<Doctor, DoctorDetailDto>();
            CreateMap<DoctorCreateUpdateDto, Doctor>();

            // Mapping từ Model Department sang DTO
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentDetailDto>();

            // Mapping từ DTO sang Model Department khi tạo hoặc cập nhật
            CreateMap<DepartmentCreateUpdateDto, Department>()

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity to DTO
            CreateMap<InventoryType, InventoryTypeDto>();

            // DTO to Entity
            CreateMap<InventoryTypeCreateUpdateDto, InventoryType>();

            // Ánh xạ Pharmacy sang PharmacyDto
            CreateMap<Pharmacy, PharmacyDto>();

            // Ánh xạ PharmacyCreateUpdateDto sang Pharmacy (dùng cho tạo mới hoặc cập nhật)
            CreateMap<PharmacyUpdateDto, Pharmacy>();
            CreateMap<PharmacyCreateDto, Pharmacy>();

            // Ánh xạ Pharmacy sang PharmacyDetailDto (chi tiết của nhà thuốc)
            CreateMap<Pharmacy, PharmacyDetailDto>();



            // Mapping từ Model ServiceGroup sang DTO cơ bản
            CreateMap<ServiceGroup, ServiceGroupDto>();

            // Mapping từ Model ServiceGroup sang DTO chi tiết
            CreateMap<ServiceGroup, ServiceGroupDetailDto>();

            // Mapping từ DTO ServiceGroupCreateUpdateDto sang Model ServiceGroup khi tạo hoặc cập nhật
            CreateMap<ServiceGroupCreateUpdateDto, ServiceGroup>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ServiceCatalog Mappings
            CreateMap<ServiceCatalog, ServiceCatalogDto>().ReverseMap();
            CreateMap<ServiceCatalog, ServiceCatalogDetailDto>().ReverseMap();
            CreateMap<ServiceCatalogCreateUpdateDto, ServiceCatalog>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


           

            // Map UnitOfMeasure -> UnitOfMeasureDetailDto
            CreateMap<UnitOfMeasure, UnitOfMeasureDetailDto>().ReverseMap();

            // Map UnitOfMeasureCreateUpdateDto -> UnitOfMeasure
            CreateMap<UnitOfMeasureCreateUpdateDto, UnitOfMeasure>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Map Supplier -> SupplierDetailDto
            CreateMap<Supplier, SupplierDetailDto>().ReverseMap();

            // Map SupplierCreateUpdateDto -> Supplier
            CreateMap<SupplierUpdateDto, Supplier>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SupplierCreateDto, Supplier>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Map country -> CountryDto
            CreateMap<Country, CountryDto>().ReverseMap();

            // Map CountryCreateUpdateDto -> Country
            CreateMap<CountryCreateUpdateDto, Country>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // Map DrugCatalog -> DrugCatalogDetailDto
            CreateMap<DrugCatalog, DrugCatalogDetailDto>();

            // Map DrugCatalogCreateUpdateDto -> DrugCatalog
            CreateMap<DrugCatalogCreateUpdateDto, DrugCatalog>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        
            //Địa chỉ 
            CreateMap<Province, ProvinceDto>().ReverseMap();
            CreateMap<District, DistrictDto>().ReverseMap();
            CreateMap<Ward, WardDto>().ReverseMap();

            // Mapping Bill to BillDto
            CreateMap<Bill, BillDto>().ReverseMap();
            //Mapping Payment to PaymentDto
            CreateMap<Payment, PaymentDto>().ReverseMap();
            // Mapping BillDetail to BillDetailDto
            CreateMap<BillDetail, BillDetailDto>().ReverseMap();

            //medical service reqest
            CreateMap<MedicalServiceRequest, MedicalServiceRequestDto>().ReverseMap();
            CreateMap<MedicalServiceRequest, CreateMedicalServiceRequestDto>().ReverseMap();
            // tiếp nhận
            //CreateMap<Patient, PatientDto>();
            CreateMap<Reception, CreateReceptionDto>().ReverseMap();
            CreateMap<ReceptionDto, Reception>().ReverseMap();

            //create-update patient
            CreateMap<AddingPatientRequest, UpdatingPatientRequest>().ReverseMap();

            //Examine khám bệnh
            CreateMap<Examine, ExamineResponseDto>().ReverseMap();
            CreateMap<ExamineRequestDto, Examine>().ReverseMap();
            CreateMap<Examine, ExamineOnly>().ReverseMap();
            CreateMap<CreateExamDto, Examine>().ReverseMap();

            CreateMap<AppUser, UserResponse>().ReverseMap();

            CreateMap<LabResult, LabResultDto>().ReverseMap();
            //Mapping Patient Model to PatientResponseDto
            CreateMap<Patient,PatientResponseDto>();

            //Mapping Employee to EmployeeDto
            CreateMap<Employee, EmployeeDto>()
            // Chỉ map những trường có tên khác nhau
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            //Mapping TechnicianModel to TechnicianDto
            CreateMap<Technician,TechnicianDto>()
           // Chỉ map những trường có tên khác nhau
           .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name))
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            //Mapping AppUserModel to UserAccountManagementDto
            CreateMap<AppUser, UserAccountResponse>();

            //
            CreateMap<OnlineAppointment, OnlineAppointmentDto>()
           .ForMember(dest => dest.DoctorName,
               opt => opt.MapFrom(src => src.Doctor.Name))  // Giả sử Doctor có trường Name
           .ForMember(dest => dest.DepartmentName,
               opt => opt.MapFrom(src => src.Doctor.Department.Name))  // Giả sử Doctor có navigation property Department
           .ForMember(dest => dest.StartTime,
               opt => opt.MapFrom(src => src.TimeSlot.StartTime));
            //labresult - xét nghiệm
            CreateMap<LabResult, LabResultDto>().ReverseMap();
            CreateMap<LabResult, CreateLabResultDto>().ReverseMap();
            CreateMap<LabResult, UpdateLabResultDto>().ReverseMap();
            // Prescription - toa thuốc - toa thuốc chi tiết
            CreateMap<Prescription, PrescriptionDto>().ReverseMap();
            CreateMap<Prescription, CreatePrescriptionDto>().ReverseMap();
            CreateMap<Prescription, UpdatePrescriptionDto>().ReverseMap();

            CreateMap<PrescriptionDetail, DetailDto>().ReverseMap();
            CreateMap<PrescriptionDetail, CreatePrescriptionDetailDto>().ReverseMap();
            CreateMap<PrescriptionDetail, UpdatePrescriptionDetailDto>().ReverseMap();
            CreateMap<CreatePrescriptionDto, UpdatePrescriptionDto>().ReverseMap();
            // toa thuốc mẫu 
            // Prescription - toa thuốc - toa thuốc chi tiết
            CreateMap<TemplatePrescription, TmplPrescriptionDto>().ReverseMap();
            CreateMap<TemplatePrescription, CreateTmplPrescriptionDto>().ReverseMap();
            CreateMap<TemplatePrescription, UpdateTmplPrescriptionDto>().ReverseMap();

            CreateMap<TemplatePrescriptionDetail, DetailDto>().ReverseMap();
            CreateMap<TemplatePrescriptionDetail, CreateTmplDetailDto>().ReverseMap();
            CreateMap<TemplatePrescriptionDetail, UpdateTmplDetailDto>().ReverseMap();
            CreateMap<CreatePrescriptionDto, UpdateTmplDetailDto>().ReverseMap();
        }
    }
}
