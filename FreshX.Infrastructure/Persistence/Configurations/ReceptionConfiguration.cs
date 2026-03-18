using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ReceptionConfiguration : IEntityTypeConfiguration<Reception>
{
    public void Configure(EntityTypeBuilder<Reception> builder)
    {
    }
}
