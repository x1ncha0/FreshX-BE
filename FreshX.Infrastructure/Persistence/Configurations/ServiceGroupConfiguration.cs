using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ServiceGroupConfiguration : IEntityTypeConfiguration<ServiceGroup>
{
    public void Configure(EntityTypeBuilder<ServiceGroup> builder)
    {
    }
}
