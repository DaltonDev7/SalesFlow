
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AmountPaid)
              .HasColumnType("decimal(10,2)");

            builder.HasOne(p => p.PaymentMethod)
             .WithMany(m => m.Payments)
             .HasForeignKey(p => p.IdPaymentMethod)
             .OnDelete(DeleteBehavior.Restrict);

            // Relación con Order
            builder.HasOne(p => p.Order)
                   .WithMany(o => o.Payments)  // Una orden puede tener varios pagos
                   .HasForeignKey(p => p.IdOrder)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
