using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Requests
{
    public class TrocaRoleRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
