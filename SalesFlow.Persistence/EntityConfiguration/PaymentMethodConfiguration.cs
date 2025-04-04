
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");
            builder.HasKey(x => x.Id);

            builder.HasMany(p => p.Payments)
              .WithOne(p => p.PaymentMethod)
              .HasForeignKey(p => p.IdPaymentMethod)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
