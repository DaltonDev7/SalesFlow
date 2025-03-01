

using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Inventory :BaseEntity
    {
        public int IdProduct { get; set; }
        public int AvailableQuantity { get; set; }
        public string UnitMeasurement { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
