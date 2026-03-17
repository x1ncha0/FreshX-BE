using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ConclusionDictionaryConfiguration : IEntityTypeConfiguration<ConclusionDictionary>
{
    public void Configure(EntityTypeBuilder<ConclusionDictionary> builder)
    {
        builder.HasOne(c => c.ServiceCatalog)
            .WithMany()
            .HasForeignKey(c => c.ServiceCatalogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
