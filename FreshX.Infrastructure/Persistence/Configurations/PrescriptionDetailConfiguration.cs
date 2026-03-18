using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class PrescriptionDetailConfiguration : IEntityTypeConfiguration<PrescriptionDetail>
{
    public void Configure(EntityTypeBuilder<PrescriptionDetail> builder)
    {
    }
}
