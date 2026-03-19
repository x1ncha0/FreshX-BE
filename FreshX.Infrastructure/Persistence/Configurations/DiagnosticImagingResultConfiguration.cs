using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class DiagnosticImagingResultConfiguration : IEntityTypeConfiguration<DiagnosticImagingResult>
{
    public void Configure(EntityTypeBuilder<DiagnosticImagingResult> builder)
    {
    }
}
