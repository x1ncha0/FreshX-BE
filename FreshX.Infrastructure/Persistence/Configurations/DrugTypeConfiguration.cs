using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class DrugTypeConfiguration : IEntityTypeConfiguration<DrugType>
{
    public void Configure(EntityTypeBuilder<DrugType> builder)
    {
    }
}
