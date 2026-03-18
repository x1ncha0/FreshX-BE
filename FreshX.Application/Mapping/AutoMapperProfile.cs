using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Dtos.Auth.Role;
using FreshX.Application.Dtos.DepartmenTypeDtos;
using FreshX.Application.Dtos.Doctor;
using FreshX.Application.Dtos.DepartmentDtos;
using FreshX.Application.Dtos.Drugs;
using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using FreshX.Application.Dtos.InventoryType;
using FreshX.Application.Dtos.Pharmacy;
using FreshX.Application.Dtos.ServiceGroup;
using FreshX.Application.Dtos.ServiceCatalog;
using FreshX.Application.Dtos.UnitOfMeasure;
using FreshX.Application.Dtos.Supplier;
using System.Diagnostics.Metrics;
using FreshX.Application.Dtos.Country;
using FreshX.Application.Dtos.DrugCatalog;
using FreshX.Application.Dtos.Payments;
using FreshX.Application.Dtos.Patient;
using FreshX.Application.Dtos.Position;
using FreshX.Application.Dtos.UserAccount;
using FreshX.Application.Dtos.Employee;
using FreshX.Application.Dtos.UserAccountManagement;
using FreshX.Application.Dtos.ExamineDtos;
using FreshX.Application.Dtos.Prescription;
using FreshX.Application.Dtos.TmplPrescription;
namespace FreshX.Application.Mapping
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
            CreateMap<DepartmentType, DepartmentTypeDto>()
                .ForMember(dest => dest.DepartmentTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0));
            CreateMap<DepartmentType, DepartmentTypeDetailDto>()
                .ForMember(dest => dest.DepartmentTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Mapping từ DTO sang Model khi tạo hoặc cập nhật
            CreateMap<DepartmentTypeCreateUpdateDto, DepartmentType>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Doctor Mappings
            CreateMap<Doctor, DoctorDto>()
             .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Id))
              // Chỉ map những trường có tên khác nhau
              .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : null))
              .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));
            CreateMap<Doctor, DoctorDetailDto>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Id));
            CreateMap<DoctorCreateUpdateDto, Doctor>();

            // Mapping từ Model Department sang DTO
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt));
            CreateMap<Department, DepartmentDetailDto>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Mapping từ DTO sang Model Department khi tạo hoặc cập nhật
            CreateMap<DepartmentCreateUpdateDto, Department>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity to DTO
            CreateMap<InventoryType, InventoryTypeDto>()
                .ForMember(dest => dest.InventoryTypeId, opt => opt.MapFrom(src => src.Id));

            // DTO to Entity
            CreateMap<InventoryTypeCreateUpdateDto, InventoryType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Ánh xạ Pharmacy sang PharmacyDto
            CreateMap<Pharmacy, PharmacyDto>()
                .ForMember(dest => dest.PharmacyId, opt => opt.MapFrom(src => src.Id));

            // Ánh xạ PharmacyCreateUpdateDto sang Pharmacy (dùng cho tạo mới hoặc cập nhật)
            CreateMap<PharmacyUpdateDto, Pharmacy>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ?? false))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PharmacyCreateDto, Pharmacy>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ?? false))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Ánh xạ Pharmacy sang PharmacyDetailDto (chi tiết của nhà thuốc)
            CreateMap<Pharmacy, PharmacyDetailDto>()
                .ForMember(dest => dest.PharmacyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));



            // Mapping từ Model ServiceGroup sang DTO cơ bản
            CreateMap<ServiceGroup, ServiceGroupDto>()
                .ForMember(dest => dest.ServiceGroupId, opt => opt.MapFrom(src => src.Id));

            // Mapping từ Model ServiceGroup sang DTO chi tiết
            CreateMap<ServiceGroup, ServiceGroupDetailDto>()
                .ForMember(dest => dest.ServiceGroupId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Mapping từ DTO ServiceGroupCreateUpdateDto sang Model ServiceGroup khi tạo hoặc cập nhật
            CreateMap<ServiceGroupCreateUpdateDto, ServiceGroup>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ServiceCatalog Mappings
            CreateMap<ServiceCatalog, ServiceCatalogDto>()
                .ForMember(dest => dest.ServiceCatalogId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ServiceCatalog, ServiceCatalogDetailDto>()
                .ForMember(dest => dest.ServiceCatalogId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HasStandardValue, opt => opt.MapFrom(src => src.ServiceStandardValueId))
                .ForMember(dest => dest.serviceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.ParentServiceName, opt => opt.MapFrom(src => src.ParentService != null ? src.ParentService.Name : null))
                .ForMember(dest => dest.ServiceGroupName, opt => opt.MapFrom(src => src.ServiceGroup != null ? src.ServiceGroup.Name : null));
            CreateMap<ServiceCatalogCreateUpdateDto, ServiceCatalog>()
                .ForMember(dest => dest.ServiceStandardValueId, opt => opt.MapFrom(src => src.HasStandardValue))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


           

            // Map UnitOfMeasure -> UnitOfMeasureDetailDto
            CreateMap<UnitOfMeasure, UnitOfMeasureDetailDto>()
                .ForMember(dest => dest.UnitOfMeasureId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map UnitOfMeasureCreateUpdateDto -> UnitOfMeasure
            CreateMap<UnitOfMeasureCreateUpdateDto, UnitOfMeasure>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Map Supplier -> SupplierDetailDto
            CreateMap<Supplier, SupplierDetailDto>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NameRussian, opt => opt.MapFrom(src => src.NameVietNam))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0));

            // Map SupplierCreateUpdateDto -> Supplier
            CreateMap<SupplierUpdateDto, Supplier>()
                .ForMember(dest => dest.NameVietNam, opt => opt.MapFrom(src => src.NameRussian))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SupplierCreateDto, Supplier>()
                .ForMember(dest => dest.NameVietNam, opt => opt.MapFrom(src => src.NameRussian))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Map country -> CountryDto
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map CountryCreateUpdateDto -> Country
            CreateMap<CountryCreateUpdateDto, Country>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => (src.IsDeleted ?? 0) == 1))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Position, PositionDto>();


            // Map DrugCatalog -> DrugCatalogDetailDto
            CreateMap<DrugCatalog, DrugCatalogDetailDto>()
                .ForMember(dest => dest.DrugCatalogId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => src.IsSuspended ? 1 : 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted ? 1 : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map DrugCatalogCreateUpdateDto -> DrugCatalog
            CreateMap<DrugCatalogCreateUpdateDto, DrugCatalog>()
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => (src.IsSuspended ?? 0) == 1))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => (src.IsDeleted ?? 0) == 1))
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
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            // Chỉ map những trường có tên khác nhau
            .ForMember(dest => dest.AvataId, opt => opt.MapFrom(src => src.AvataId))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : null))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

            //Mapping TechnicianModel to TechnicianDto
            CreateMap<Technician,TechnicianDto>()
           .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.Id))
           // Chỉ map những trường có tên khác nhau
           .ForMember(dest => dest.AvataId, opt => opt.MapFrom(src => src.AvataId))
           .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : null))
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

            //Mapping AppUserModel to UserAccountManagementDto
            CreateMap<AppUser, UserAccountResponse>();

            //
            CreateMap<OnlineAppointment, OnlineAppointmentDto>()
           .ForMember(dest => dest.DoctorName,
               opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : null))  // Giả sử Doctor có trường Name
            .ForMember(dest => dest.DepartmentName,
               opt => opt.MapFrom(src => src.Doctor != null && src.Doctor.Department != null ? src.Doctor.Department.Name : null))  // Giả sử Doctor có navigation property Department
            .ForMember(dest => dest.StartTime,
               opt => opt.MapFrom(src => src.TimeSlot != null ? src.TimeSlot.StartTime : default));
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

