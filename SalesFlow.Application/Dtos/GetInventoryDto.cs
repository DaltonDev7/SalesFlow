

namespace SalesFlow.Application.Dtos
{
    public class GetInventoryDto
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int AvailableQuantity { get; set; }
        public string UnitMeasurement { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
