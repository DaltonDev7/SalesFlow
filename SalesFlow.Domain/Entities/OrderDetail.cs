
using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
