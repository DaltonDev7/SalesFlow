
using SalesFlow.Domain.Common;
using SalesFlow.Domain.Enums;

namespace SalesFlow.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int IdCustomer { get; set; }
        public int IdUser { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total {  get; set; }
        public OrderStatus StatusOrder { get; set; } = OrderStatus.Pendiente;
        public OrderType OrderType { get; set; }
    }
}
