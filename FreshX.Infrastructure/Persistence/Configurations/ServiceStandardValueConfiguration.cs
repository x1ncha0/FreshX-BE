using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ServiceStandardValueConfiguration : IEntityTypeConfiguration<ServiceStandardValue>
{
    public void Configure(EntityTypeBuilder<ServiceStandardValue> builder)
    {
    }
}
