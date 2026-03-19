using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class DiseaseGroupConfiguration : IEntityTypeConfiguration<DiseaseGroup>
{
    public void Configure(EntityTypeBuilder<DiseaseGroup> builder)
    {
    }
}
