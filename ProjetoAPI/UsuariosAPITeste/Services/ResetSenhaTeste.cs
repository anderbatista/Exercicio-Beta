using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UsuariosAPI.Controllers;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;
using UsuariosAPI.Services;
using Xunit;

namespace UsuariosAPITeste.Services
{
    public class ResetSenhaTeste
    {
        private ResetSenhaService resetSenhaService;
        

        public ResetSenhaTeste()
        {
            resetSenhaService = new ResetSenhaService(new Mock<SignInManager<CustomIdentityUser>>().Object);
        }
        [Fact]
        public void TesteResetSenha()
        {
            //Arrange
            var resetTeste = new ResetSenhaRequest()
            {

                Email = "andersonTeste@clearTech.dev" ,
                Password = "Senha@123",
                NewPassword = "Ctech@123",
                NewRePassword = "Ctech@123"
            };
            //Act
            var controller = new ResetSenhaController(resetSenhaService);
            var resposta = (ObjectResult)controller.ResetSenha(resetTeste);
            //Assert
            Assert.Equal(200, resposta.StatusCode);

        }
        


    }
}
