using System.ComponentModel.DataAnnotations;

namespace SalesFlow
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "El correo es requerido.")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Password { get; set; } = null!;
    }
}
