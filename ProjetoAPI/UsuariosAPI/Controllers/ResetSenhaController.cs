using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResetSenhaController : ControllerBase
    {
        private ResetSenhaService _resetService;

        public ResetSenhaController(ResetSenhaService resetService)
        {
            _resetService = resetService;
        }
        [HttpPost]
        public IActionResult ResetSenha([FromBody] ResetSenhaRequest resetSenha)
        {
            Result resultado = _resetService.ResetSenha(resetSenha);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Reasons);
            }
            return Ok();
        }
    }
}
