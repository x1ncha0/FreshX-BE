using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Xml;

namespace Freshx_API.Models;

public partial class FreshxDBContext : IdentityDbContext<AppUser,IdentityRole,string>
{
    public FreshxDBContext(DbContextOptions<FreshxDBContext> options)
        : base(options)
    {
    }
   
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }  // Or perhaps a more descriptive name like "ApplicationUsers"
    public DbSet<ConclusionDictionary> ConclusionDictionaries { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentType> DepartmentTypes { get; set; }
    public DbSet<DiagnosisDictionary> DiagnosisDictionaries { get; set; }
    public DbSet<DiagnosticImagingResult> DiagnosticImagingResults { get; set; }
    public DbSet<DiseaseGroup> DiseaseGroups { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<DrugBooking> DrugBookings { get; set; }
    public DbSet<DrugCatalog> DrugCatalogs { get; set; }
    public DbSet<DrugType> DrugTypes { get; set; }
    public DbSet<EmailContent> EmailContents { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Clinic> Hospitals { get; set; }
    public DbSet<ICDCatalog> ICDcatalogs { get; set; } // Follow C# capitalization conventions: IcdCatalogs
    public DbSet<Icdchapter> Icdchapters { get; set; } // IcdChapters
    public DbSet<InventoryType> InventoryTypes { get; set; }
    public DbSet<Examine> Examines { get; set; }
    public DbSet<LabResult> LabResults { get; set; }
    public DbSet<MedicalServiceRequest> MedicalServiceRequests { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuParent> MenuPermissions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Pharmacy> Pharmacies { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionDetail> PrescriptionDetail { get; set; }
    public DbSet<Reception> Receptions { get; set; }
    public DbSet<Savefile> Savefiles { get; set; } // Consider a more descriptive name
    public DbSet<ServiceCatalog> ServiceCatalogs { get; set; }
    public DbSet<ServiceGroup> ServiceGroups { get; set; }
    public DbSet<ServiceStandardValue> ServiceStandardValues { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    public DbSet<TemplatePrescription> TemplatePrescriptions { get; set; }
    public DbSet<TemplatePrescriptionDetail> TemplatePrescriptionDetails { get; set; }
    public DbSet<ServiceTypes> ServiceTypes { get; set; }
    public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Ward> Wards { get; set; }
 
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<Bill> Bills { get; set; } // Bảng hóa đơn
    public DbSet<BillDetail> BillDetails { get; set; } // Bảng chi tiết hóa đơn
    public DbSet<Payment> Payments { get; set; } // Bảng thanh toán

    public DbSet<Position> Positions { get; set; }

    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<OnlineAppointment> OnlineAppointments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure foreign keys and relationships
        //  ICDCatalog
        modelBuilder.Entity< ICDCatalog>()
            .HasOne(i => i. ICDCatalogGroup)
            .WithMany()
            .HasForeignKey(i => i. ICDCatalogGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        // ConclusionDictionary
        modelBuilder.Entity<ConclusionDictionary>()
            .HasOne(c => c.ServiceCatalog)
            .WithMany()
            .HasForeignKey(c => c.ServiceCatalogId)
            .OnDelete(DeleteBehavior.Restrict);

      
        // ServiceCatalog
        modelBuilder.Entity<ServiceCatalog>()
            .HasOne(s => s.ServiceGroup)
            .WithMany()
            .HasForeignKey(s => s.ServiceGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ServiceCatalog>()
            .HasOne(s => s.ParentService)
            .WithMany()
            .HasForeignKey(s => s.ParentServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        // service group

        modelBuilder.Entity<ServiceGroup>()
           .HasMany(sg => sg.ServiceCatalogs)
           .WithOne(sc => sc.ServiceGroup)
           .HasForeignKey(sc => sc.ServiceCatalogId);

        //phamacy
        modelBuilder.Entity<Pharmacy>()
            .HasMany(e => e.Department)
            .WithOne()
            .HasForeignKey(d => d.DepartmentId); // Cấu hình khóa ngoại rõ ràng

        modelBuilder.Entity<Pharmacy>()
    .HasMany(e => e.InventoryType)
    .WithOne()
    .HasForeignKey(d => d.InventoryTypeId); // Cấu hình khóa ngoại rõ ràng


        modelBuilder.Entity<Bill>()
                .HasMany(b => b.BillDetails)
                .WithOne(d => d.Bill)
                .HasForeignKey(d => d.BillId);

        modelBuilder.Entity<Bill>()
            .HasMany(b => b.Payments)
            .WithOne(p => p.Bill)
            .HasForeignKey(p => p.BillId);


        modelBuilder.Entity<LabResult>(b =>
        {
            b.HasKey(e => e.LabResultId);
            b.Property(e => e.LabResultId).ValueGeneratedOnAdd();
        });

        //phòng ban vs loại phòng ban
        modelBuilder.Entity<Department>()
      .HasKey(d => d.DepartmentId);

        modelBuilder.Entity<Department>()
            .Property(d => d.DepartmentId)
            .ValueGeneratedOnAdd();


        modelBuilder.Entity<AppUser>()
        .HasOne(u => u.Doctor)
        .WithOne(d => d.AppUser)
        .HasForeignKey<Doctor>(d => d.AccountId)
        .IsRequired(false)                          // FK không được null
        .OnDelete(DeleteBehavior.Cascade)      // Xóa cascade
        .OnDelete(DeleteBehavior.SetNull)      // Set null khi xóa
        .OnDelete(DeleteBehavior.Restrict);    // Ngăn xóa nếu có quan hệ

        modelBuilder.Entity<AppUser>()
       .HasOne(u => u.Patient)
       .WithOne(p => p.AppUser)
       .HasForeignKey<Patient>(p => p.AccountId)
       .IsRequired(false)                          // FK được null
       .OnDelete(DeleteBehavior.Cascade)      // Xóa cascade
       .OnDelete(DeleteBehavior.SetNull)      // Set null khi xóa
       .OnDelete(DeleteBehavior.Restrict);    // Ngăn xóa nếu có quan hệ

        modelBuilder.Entity<AppUser>()
       .HasOne(u => u.Technician)
       .WithOne(t => t.AppUser)
       .HasForeignKey<Technician>(t => t.AccountId)
       .IsRequired(false)                          // FK không được null
       .OnDelete(DeleteBehavior.Cascade)      // Xóa cascade
       .OnDelete(DeleteBehavior.SetNull)      // Set null khi xóa
       .OnDelete(DeleteBehavior.Restrict);    // Ngăn xóa nếu có quan hệ

        modelBuilder.Entity<AppUser>()
       .HasOne(u => u.Employee)
       .WithOne(e => e.AppUser)
       .HasForeignKey<Employee>(e => e.AccountId)
       .IsRequired(false)                          // FK được null
       .OnDelete(DeleteBehavior.Cascade)      // Xóa cascade
       .OnDelete(DeleteBehavior.SetNull)      // Set null khi xóa
       .OnDelete(DeleteBehavior.Restrict);    // Ngăn xóa nếu có quan hệ
      /*  modelBuilder.Entity<TimeSlot>()
              .Property(t => t.Duration)
              .HasComputedColumnSql("DATEADD(MINUTE, DATEDIFF(MINUTE, StartTime, EndTime), CAST('00:00:00' AS TIME))");*/


        modelBuilder.Entity<OnlineAppointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OnlineAppointment>()
            .HasOne(a => a.AppUser)
            .WithMany()
            .HasForeignKey(a => a.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OnlineAppointment>()
            .HasOne(a => a.TimeSlot)
            .WithMany()
            .HasForeignKey(a => a.TimeSlotId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
