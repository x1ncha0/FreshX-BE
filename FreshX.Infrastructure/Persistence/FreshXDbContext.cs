using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Persistence;

public class FreshXDbContext(DbContextOptions<FreshXDbContext> options)
    : IdentityDbContext<AppUser, IdentityRole, string>(options)
{
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<BillDetail> BillDetails => Set<BillDetail>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Clinic> Clinics => Set<Clinic>();
    public DbSet<ConclusionDictionary> ConclusionDictionaries => Set<ConclusionDictionary>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<DepartmentType> DepartmentTypes => Set<DepartmentType>();
    public DbSet<DiagnosisDictionary> DiagnosisDictionaries => Set<DiagnosisDictionary>();
    public DbSet<DiagnosticImagingResult> DiagnosticImagingResults => Set<DiagnosticImagingResult>();
    public DbSet<DiseaseGroup> DiseaseGroups => Set<DiseaseGroup>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<DrugBooking> DrugBookings => Set<DrugBooking>();
    public DbSet<DrugCatalog> DrugCatalogs => Set<DrugCatalog>();
    public DbSet<DrugType> DrugTypes => Set<DrugType>();
    public DbSet<EmailContent> EmailContents => Set<EmailContent>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Examine> Examines => Set<Examine>();
    public DbSet<ICDCatalog> ICDCatalogs => Set<ICDCatalog>();
    public DbSet<Icdchapter> IcdChapters => Set<Icdchapter>();
    public DbSet<InventoryType> InventoryTypes => Set<InventoryType>();
    public DbSet<LabResult> LabResults => Set<LabResult>();
    public DbSet<MedicalServiceRequest> MedicalServiceRequests => Set<MedicalServiceRequest>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<MenuParent> MenuParents => Set<MenuParent>();
    public DbSet<OnlineAppointment> OnlineAppointments => Set<OnlineAppointment>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Pharmacy> Pharmacies => Set<Pharmacy>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionDetail> PrescriptionDetails => Set<PrescriptionDetail>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<Reception> Receptions => Set<Reception>();
    public DbSet<Savefile> Savefiles => Set<Savefile>();
    public DbSet<ServiceCatalog> ServiceCatalogs => Set<ServiceCatalog>();
    public DbSet<ServiceGroup> ServiceGroups => Set<ServiceGroup>();
    public DbSet<ServiceStandardValue> ServiceStandardValues => Set<ServiceStandardValue>();
    public DbSet<ServiceTypes> ServiceTypes => Set<ServiceTypes>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Technician> Technicians => Set<Technician>();
    public DbSet<TemplatePrescription> TemplatePrescriptions => Set<TemplatePrescription>();
    public DbSet<TemplatePrescriptionDetail> TemplatePrescriptionDetails => Set<TemplatePrescriptionDetail>();
    public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
    public DbSet<UnitOfMeasure> UnitOfMeasures => Set<UnitOfMeasure>();
    public DbSet<Ward> Wards => Set<Ward>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FreshXDbContext).Assembly);
    }
}
