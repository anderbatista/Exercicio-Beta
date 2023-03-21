using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroUserController : ControllerBase
    {
        private CadastroUserService _cadastroUserService;
        public CadastroUserController(CadastroUserService cadastroUserService)
        {
            _cadastroUserService = cadastroUserService;
        }

        [HttpPost("cliente")]
        public async Task<IActionResult> CadastraCliente([FromBody] CreateUsuarioDto createDto)
        {
            Result resultado = await _cadastroUserService.AddUserCliente(createDto);
            if (resultado.IsFailed) return BadRequest(resultado.Reasons);
            return Ok("Cliente criado com sucesso!");
        }

        [HttpPost("lojista")]
        public async Task<IActionResult> CadastraLojista([FromBody] CreateUsuarioDto createDto)
        {
            Result resultado = await _cadastroUserService.AddUserLojista(createDto);
            if (resultado.IsFailed) return BadRequest(resultado.Reasons);
            return Ok("Lojista criado com sucesso!");
        }
    }
}