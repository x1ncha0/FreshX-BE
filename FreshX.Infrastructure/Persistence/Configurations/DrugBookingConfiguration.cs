using FreshX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshX.Infrastructure.Persistence.Configurations;

public sealed class DrugBookingConfiguration : IEntityTypeConfiguration<DrugBooking>
{
    public void Configure(EntityTypeBuilder<DrugBooking> builder)
    {
    }
}
