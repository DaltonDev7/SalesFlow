
using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Tables : BaseEntity
    {
        public string Name {  get; set; }

        public int Capacity { get; set;}

        public string StatusTable { get; set;}



        public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();


    }
}
