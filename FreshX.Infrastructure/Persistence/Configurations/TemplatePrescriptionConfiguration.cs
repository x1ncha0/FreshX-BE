using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class TemplatePrescriptionConfiguration : IEntityTypeConfiguration<TemplatePrescription>
{
    public void Configure(EntityTypeBuilder<TemplatePrescription> builder)
    {
    }
}
