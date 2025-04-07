
namespace SalesFlow.Application.Dtos.Authentication
{
    public class GetUserAuth
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Names { get; set; } = null!;
        public string LastNames { get; set; } = null!;
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public List<RolDto> Roles { get; set; }

    }
}
