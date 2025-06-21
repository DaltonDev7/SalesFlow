using SalesFlow.Domain.Common;


namespace SalesFlow.Domain.Entities
{
    public class Customer : BaseEntity
    {

        public string? Email { get; set; }

        public string? Password { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        // Relación con Order (1 a muchos)
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
