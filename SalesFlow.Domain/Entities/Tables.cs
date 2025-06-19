
using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Tables : BaseEntity
    {
        public int IdCustomer {  get; set; }

        public int Capacity { get; set;}

        public string StatusTable { get; set;}
    }
}
