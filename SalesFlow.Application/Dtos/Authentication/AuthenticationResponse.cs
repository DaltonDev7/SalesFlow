
namespace SalesFlow.Application.Dtos.Authentication
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public List<RolDto> Roles { get; set; }

        public string? JwtToken { get; set; }
    }
}
