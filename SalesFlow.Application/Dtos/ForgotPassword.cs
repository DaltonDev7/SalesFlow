
namespace SalesFlow.Application.Dtos
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; } = null!;
    }

    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;      // el token recibido por correo (URL-encoded)
        public string NewPassword { get; set; } = null!;
    }

}
