
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class ReservationsConfigurations : IEntityTypeConfiguration<Reservations>
    {
        public void Configure(EntityTypeBuilder<Reservations> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Reservations");
        }
    }
}
