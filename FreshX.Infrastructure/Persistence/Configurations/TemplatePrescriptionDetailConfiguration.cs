using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class TemplatePrescriptionDetailConfiguration : IEntityTypeConfiguration<TemplatePrescriptionDetail>
{
    public void Configure(EntityTypeBuilder<TemplatePrescriptionDetail> builder)
    {
    }
}
