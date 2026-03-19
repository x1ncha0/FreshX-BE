using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class InventoryTypeConfiguration : IEntityTypeConfiguration<InventoryType>
{
    public void Configure(EntityTypeBuilder<InventoryType> builder)
    {
    }
}
