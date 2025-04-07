using Microsoft.AspNetCore.Identity;

namespace SalesFlow.Identity.Entities
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

        //public ICollection<ApplicationUserRoles> UserRoles { get; set; } = new List<ApplicationUserRoles>();
    }

}
