using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class ExamineConfiguration : IEntityTypeConfiguration<Examine>
{
    public void Configure(EntityTypeBuilder<Examine> builder)
    {
    }
}
