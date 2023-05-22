using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Requests
{
    public class ResetSenhaRequest
    {
        [Required]
        public string Email { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
        [Required][DataType(DataType.Password)] public string NewPassword { get; set; }
        [Required][Compare("NewPassword", ErrorMessage = "Verifique a senha digitada")] public string NewRePassword { get; set; }
    }
}
