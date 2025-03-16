using System.ComponentModel.DataAnnotations;

namespace SalesFlowApp.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
