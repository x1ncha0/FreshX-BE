using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class IcdchapterConfiguration : IEntityTypeConfiguration<Icdchapter>
{
    public void Configure(EntityTypeBuilder<Icdchapter> builder)
    {
    }
}
