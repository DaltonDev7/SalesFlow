
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

        
            builder.HasOne(r => r.Table)
                   .WithMany(p => p.Reservations)
                   .HasForeignKey(r => r.IdTable)
                    .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(r => r.Customer)
                  .WithMany(p => p.Reservations)
                  .HasForeignKey(r => r.IdCustomer)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
