using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class SavefileConfiguration : IEntityTypeConfiguration<Savefile>
{
    public void Configure(EntityTypeBuilder<Savefile> builder)
    {
    }
}
