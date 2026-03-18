using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class MenuParentConfiguration : IEntityTypeConfiguration<MenuParent>
{
    public void Configure(EntityTypeBuilder<MenuParent> builder)
    {
    }
}
