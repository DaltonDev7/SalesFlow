

using SalesFlow.Domain.Enums;

namespace SalesFlow.Application.Dtos
{
    public class GetOrdersDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string EmployeName { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public string StatusOrder { get; set; }
        public string OrderType { get; set; }
    }
}
