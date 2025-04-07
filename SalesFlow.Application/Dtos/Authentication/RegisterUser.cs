

namespace SalesFlow.Application.Dtos.Authentication
{
    public class RegisterUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Names { get; set; } = null!;
        public string LastNames { get; set; } = null!;
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public int IdRol { get; set; }

    }
}
