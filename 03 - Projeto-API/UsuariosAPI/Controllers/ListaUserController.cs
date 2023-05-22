using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListaUserController : ControllerBase
    {
        private ListaUsuarioService _listaUserService;

        public ListaUserController(ListaUsuarioService listaUserService)
        {
            _listaUserService = listaUserService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ListaUsuario([FromQuery] string username, [FromQuery] string cpf, 
            [FromQuery] string email, [FromQuery] bool? status)
        {
            var result = await _listaUserService.ListarUsuario(username, cpf, email, status);
            return Ok(result);
        }
    }
}
