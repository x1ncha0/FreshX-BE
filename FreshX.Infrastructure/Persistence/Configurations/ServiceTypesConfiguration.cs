using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ServiceTypesConfiguration : IEntityTypeConfiguration<ServiceTypes>
{
    public void Configure(EntityTypeBuilder<ServiceTypes> builder)
    {
    }
}
