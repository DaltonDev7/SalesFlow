

namespace SalesFlow.Application.Dtos
{
    public class GetOrderDetailDto
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string ProductName { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
