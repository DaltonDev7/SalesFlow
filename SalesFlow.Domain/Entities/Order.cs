
using SalesFlow.Domain.Common;
using SalesFlow.Domain.Enums;

namespace SalesFlow.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int IdCustomer { get; set; }
        public int IdEmploye { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total {  get; set; }
        public OrderStatus StatusOrder { get; set; } = OrderStatus.PENDIENTE;
        public OrderType OrderType { get; set; }


        // Relación con Customer (Muchos a uno)
        public virtual Customer Customer { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Payments> Payments { get; set; }
    }
}
