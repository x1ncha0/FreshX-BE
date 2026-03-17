using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ICDCatalogConfiguration : IEntityTypeConfiguration<ICDCatalog>
{
    public void Configure(EntityTypeBuilder<ICDCatalog> builder)
    {
        builder.HasOne(i => i.ICDCatalogGroup)
            .WithMany()
            .HasForeignKey(i => i.ICDCatalogGroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
