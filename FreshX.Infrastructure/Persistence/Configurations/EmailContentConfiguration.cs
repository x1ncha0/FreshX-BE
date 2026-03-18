using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class EmailContentConfiguration : IEntityTypeConfiguration<EmailContent>
{
    public void Configure(EntityTypeBuilder<EmailContent> builder)
    {
    }
}
