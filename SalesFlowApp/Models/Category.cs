namespace SalesFlowApp.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
