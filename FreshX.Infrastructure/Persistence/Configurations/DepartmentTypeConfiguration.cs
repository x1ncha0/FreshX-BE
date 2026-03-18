using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class DepartmentTypeConfiguration : IEntityTypeConfiguration<DepartmentType>
{
    public void Configure(EntityTypeBuilder<DepartmentType> builder)
    {
    }
}
