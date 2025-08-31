

using SalesFlow.Domain.Enums;

namespace SalesFlow.Application.Dtos
{
    public class GetOrdersDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNameV2 { get; set; }
        public int? IdPaymentMethod { get; set; }

        public int? IdCustomer { get; set; }

        public string EmployeName { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public int StatusOrder { get; set; }
        public string OrderType { get; set; }

    }
}
