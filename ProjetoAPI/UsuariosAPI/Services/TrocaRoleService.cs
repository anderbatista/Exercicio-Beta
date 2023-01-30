using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TrocaRoleService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        public TrocaRoleService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result TrocaRole(TrocaRoleRequest trocaRole)
        {
            var buscaUser = _signInManager.UserManager.FindByEmailAsync(trocaRole.Email).Result;

            if(buscaUser == null)
            {
                return Result.Fail("Usuário não encontrado");
            }

            var verificaUserCliente = _signInManager.UserManager.IsInRoleAsync(buscaUser, "cliente");

            if(verificaUserCliente.Result == true)
            {
                return Result.Fail("Clientes não podem ter permissões alteradas");
            }

            var trocandoRole = _signInManager.UserManager.AddToRoleAsync(buscaUser, trocaRole.Role).Result;

            if (trocandoRole.Succeeded)
            {
                return Result.Ok();
            }

            return Result.Fail("Erro ao alterar permissão do usuário.");

        }

    }
}
