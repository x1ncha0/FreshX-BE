using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasOne(u => u.Doctor)
            .WithOne(d => d.AppUser)
            .HasForeignKey<Doctor>(d => d.AccountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Patient)
            .WithOne(p => p.AppUser)
            .HasForeignKey<Patient>(p => p.AccountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Employee)
            .WithOne(e => e.AppUser)
            .HasForeignKey<Employee>(e => e.AccountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Technician)
            .WithOne(t => t.AppUser)
            .HasForeignKey<Technician>(t => t.AccountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
