
namespace SalesFlow.Application.Dtos
{
    public class CategorySalesDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalItemsSold { get; set; }
    }

}
