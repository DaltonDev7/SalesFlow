

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrdersDetails");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitPrice)
                 .HasColumnType("decimal(10,2)");

            builder.Property(x => x.SubTotal)
                .HasColumnType("decimal(10,2)");

        }
    }
}
