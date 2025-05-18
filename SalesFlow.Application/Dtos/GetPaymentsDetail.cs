namespace SalesFlow.Application.Dtos
{
    public class GetPaymentsDetail
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CustomerName { get; set; }
        public string EmployeName { get; set; }
        public string OrderType { get; set; }
    }
}
