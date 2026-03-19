using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class DiagnosisDictionaryConfiguration : IEntityTypeConfiguration<DiagnosisDictionary>
{
    public void Configure(EntityTypeBuilder<DiagnosisDictionary> builder)
    {
    }
}
