
using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Payments : BaseEntity
    {
        public int IdOrder { get; set; }
        public int IdPaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
