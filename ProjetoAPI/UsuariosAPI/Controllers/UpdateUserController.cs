using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateUserController : ControllerBase
    {
        private UpdateUserService _updateUserService;

        public UpdateUserController(UpdateUserService updateUserService)
        {
            _updateUserService = updateUserService;
        }
        [HttpPut]
        [Authorize(Roles = "cliente, lojista, admin")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] UpdateUsuarioDto userDto)
        {
            Result resultado = await _updateUserService.EditarUser(id, userDto);
            if (resultado.IsFailed) return BadRequest(resultado.Reasons);
            return Ok("Usuário alterado");
        }
    }
}
