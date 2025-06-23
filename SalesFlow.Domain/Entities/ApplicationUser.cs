
using Microsoft.AspNetCore.Identity;

namespace SalesFlow.Domain.Entities
{

    public class ApplicationUser : IdentityUser<int>
    {
        public string Names { get; set; } = null!;
        public string LastNames { get; set; } = null!;
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual ICollection<Order> CustomerOrders { get; set; } = new List<Order>();
        public virtual ICollection<Order> EmployeeOrders { get; set; } = new List<Order>();
        public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
    }

}
