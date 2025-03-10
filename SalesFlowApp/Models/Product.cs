namespace SalesFlowApp.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public int? Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public Boolean Available { get; set; } = true;
    }
}
