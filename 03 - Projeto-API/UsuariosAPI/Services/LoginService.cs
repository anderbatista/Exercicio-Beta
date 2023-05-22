using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email);
            if (usuario.Result == null) return Result.Fail("E-mail incorreto!");

            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(usuario.Result.UserName, request.Password, false, false);

            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(u =>
                    u.NormalizedUserName == usuario.Result.UserName.ToUpper());
                
                Token token = _tokenService
                    .CreateToken(identityUser, _signInManager
                    .UserManager
                    .GetRolesAsync(identityUser)
                    .Result.FirstOrDefault());

                return Result.Ok().WithSuccess(token.Value);
            } 
            return Result.Fail("Usuário e senha incorretos");
        }
    }
}
