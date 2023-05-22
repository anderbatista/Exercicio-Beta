using Moq;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using Xunit;
using AutoMapper;
using ProjetoAPI.Services;
using ProjetoAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TesteAPI.Services
{
    public class CategoriaTeste
    {
        private CategoriaService categoriaService;
        private Mock<ICategoriaDao> categoriaDao;
        private Mock<ISubcategoriaDao> subcategoriaDao;
        public CategoriaTeste()
        {
            categoriaService = new CategoriaService(new Mock<ICategoriaDao>().Object, new Mock<ISubcategoriaDao>().Object, new Mock<IMapper>().Object);
            categoriaDao = new Mock<ICategoriaDao>();
            subcategoriaDao = new Mock<ISubcategoriaDao>();
        }

        [Fact]
        public void Cadastrar_Valida_Criterios_Metodo()
        {
            //Arrange
            var exDataCriacao = $"{DateTime.Now:yyyy-MM-dd}";
            var exStatus = true;
            //Act
            var resultado = categoriaService.AddCategoria(new CreateCategoriaDto { Nome = "TesteAdicionar" });
            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(exDataCriacao, $"{resultado.DataCriacao:yyyy-MM-dd}");
            Assert.Equal(exStatus, resultado.Status);
        }

        [Fact]
        public void Cadastrar_Verifica_Data_Criacao()
        {
            //Arrange
            var agora = $"{DateTime.Now:yyyy-MM-dd}";
            //Act
            var resultado = categoriaService.AddCategoria(new CreateCategoriaDto { Nome = "TesteDataCriacao" });
            //Assert
            Assert.Equal(agora, $"{resultado.DataCriacao:yyyy-MM-dd}");
        }

        [Fact]
        public void Cadastrar_Verifica_Status_true()
        {
            //Arrange
            var categoriaTeste = new CreateCategoriaDto()
            {
                Nome = "TesteStatusTrue",
                Status = true
            };
            //Act
            var controller = new CategoriaController(categoriaService);
            var resposta = (ObjectResult)controller.CadastrarCategorias(categoriaTeste);
            //Assert
            Assert.Equal(201, resposta.StatusCode);
        }

        [Fact]
        public void Cadastrar_Verifica_Status_false()
        {
            //Arrange
            var categoriaTeste = new CreateCategoriaDto()
            {
                Nome = "TesteStatusFalse",
                Status = false
            };
            //Act
            var controller = new CategoriaController(categoriaService);
            var resposta = (ObjectResult)controller.CadastrarCategorias(categoriaTeste);
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }

        [Fact]
        public void Cadastrar__128Caracteres_201_Created()
        {
            //Arrange
            var categoriaTeste = new CreateCategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaa" + // 20
                       "aaaaaaaaaaaaaaaaaaaa" + // 40
                       "aaaaaaaaaaaaaaaaaaaa" + // 60 
                       "aaaaaaaaaaaaaaaaaaaa" + // 80
                       "aaaaaaaaaaaaaaaaaaaa" + // 100
                       "aaaaaaaaaaaaaaaaaaaa" + // 120
                       "aaaaaaaa"
            };
            //Act
            var controller = new CategoriaController(categoriaService);
            var resposta = (ObjectResult)controller.CadastrarCategorias(categoriaTeste);
            //Assert
            Assert.Equal(201, resposta.StatusCode);
        }

        [Fact]
        public void Cadastrar_Excede_Caracteres_400_BadRequest()
        {
            //Arrange
            var categoriaTeste = new CreateCategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaa" + // 20
                       "aaaaaaaaaaaaaaaaaaaa" + // 40
                       "aaaaaaaaaaaaaaaaaaaa" + // 60 
                       "aaaaaaaaaaaaaaaaaaaa" + // 80
                       "aaaaaaaaaaaaaaaaaaaa" + // 100
                       "aaaaaaaaaaaaaaaaaaaa" + // 120
                       "aaaaaaaaaaaaa"
            };
            //Act
            var controller = new CategoriaController(categoriaService);
            var resposta = (ObjectResult)controller.CadastrarCategorias(categoriaTeste);
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }

        [Fact]
        public void Cadastrar_Caracteres_NuloOuVazio_400_BadRequest()
        {
            //Arrange
            var categoriaTeste = new CreateCategoriaDto()
            {
                Nome = ""
            };
            //Act
            var controller = new CategoriaController(categoriaService);
            var resposta = (ObjectResult)controller.CadastrarCategorias(categoriaTeste);
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }
    }
}
