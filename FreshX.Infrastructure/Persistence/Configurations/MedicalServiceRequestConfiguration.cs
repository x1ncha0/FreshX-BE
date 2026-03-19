using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class MedicalServiceRequestConfiguration : IEntityTypeConfiguration<MedicalServiceRequest>
{
    public void Configure(EntityTypeBuilder<MedicalServiceRequest> builder)
    {
    }
}
