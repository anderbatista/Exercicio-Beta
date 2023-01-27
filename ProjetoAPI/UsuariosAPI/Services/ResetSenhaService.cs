using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class ResetSenhaService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public ResetSenhaService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result ResetSenha(ResetSenhaRequest resetSenha) 
        {
            var buscaUser = _signInManager.UserManager.FindByEmailAsync(resetSenha.Email).Result;
            var TrocaSenha = _signInManager.UserManager.ChangePasswordAsync(buscaUser, resetSenha.Password, resetSenha.NewPassword).Result;
            if(TrocaSenha.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Erro ao alterar a senha");
        }
    }
}
