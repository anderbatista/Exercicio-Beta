using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrocaRoleController : ControllerBase
    {
        private TrocaRoleService _trocaRoleService;

        public TrocaRoleController(TrocaRoleService trocaRoleService)
        {
            _trocaRoleService = trocaRoleService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult TrocaRole(TrocaRoleRequest trocaRole)
        {
            Result resultado = _trocaRoleService.TrocaRole(trocaRole);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors.FirstOrDefault());
            }
            return Ok("Permissão alterada com sucesso!");
        }
    }
}
