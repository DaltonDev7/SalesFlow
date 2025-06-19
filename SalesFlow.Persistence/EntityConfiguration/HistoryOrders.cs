

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class HistoryOrdersConfiguration : IEntityTypeConfiguration<HistoryOrders>
    {
        public void Configure(EntityTypeBuilder<HistoryOrders> builder)
        {
            builder.ToTable("HistoryOrders");
            builder.HasKey(x => x.Id);
        }
    }
}
