using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ServiceCatalogConfiguration : IEntityTypeConfiguration<ServiceCatalog>
{
    public void Configure(EntityTypeBuilder<ServiceCatalog> builder)
    {
        builder.HasOne(s => s.ServiceGroup)
            .WithMany(g => g.ServiceCatalogs)
            .HasForeignKey(s => s.ServiceGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.ParentService)
            .WithMany(p => p.ChildServices)
            .HasForeignKey(s => s.ParentServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
