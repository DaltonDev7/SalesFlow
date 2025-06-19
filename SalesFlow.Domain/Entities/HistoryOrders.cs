
using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class HistoryOrders : BaseEntity
    {
        public DateTime Fecha { get; set; } 
        public int IdOrder { get; set; }

        public string NameCustomer { get; set; }

        public int Total { get; set; }
        public string MethodPayment { get; set; }

        public string OrderType { get; set; }
    }
}
