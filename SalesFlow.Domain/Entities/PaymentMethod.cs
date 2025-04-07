
using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class PaymentMethod : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<Payments> Payments { get; set; }
    }
}
