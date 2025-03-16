using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal? Price { get; set; } = null;


        [Required(ErrorMessage = "Debes seleccionar una categoría")]
        public int? IdCategoria { get; set; } = null;
        public Boolean Available { get; set; } = true;
    }
}
