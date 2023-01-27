using AutoMapper;
using Moq;
using ProjetoAPI.Data;
using ProjetoAPI.Models;
using ProjetoAPI.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Profiles;
using ProjetoAPI.Controllers;
using System;

namespace TesteAPI.Services
{
    public class SubcategoriaTeste
    {
        readonly Mock<ICategoriaDao> categoriaDao;
        readonly Mock<ISubcategoriaDao> subcategoriaDao;
        readonly Mock<IProdutoDao> produtoDao;
        readonly IMapper _mapper;
        MapperConfiguration _MapperConfiguration;
        public SubcategoriaTeste()
        {
            categoriaDao = new Mock<ICategoriaDao>();
            subcategoriaDao = new Mock<ISubcategoriaDao>();
            produtoDao = new Mock<IProdutoDao>();

            _MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SubcategoriaProfile());
            });
            _mapper = _MapperConfiguration.CreateMapper();
        }
        [Fact]
        public void Cadastro_Valida_Criterios_Metodo()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo", DataCriacao = DateTime.Now, Status = true });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var exDataCriacao = $"{DateTime.Now:yyyy-MM-dd}";
            var exStatus = true;
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() { Nome = "Nome" });
            var subcategorias = Assert.IsAssignableFrom<Subcategoria>(resposta.Value);
            //Assert
            Assert.NotNull(subcategorias);
            Assert.Equal(exDataCriacao, $"{subcategorias.DataCriacao:yyyy-MM-dd}");
            Assert.Equal(exStatus, subcategorias.Status);
        }
        [Fact]
        public void Cadastro_Verifica_Data_Criacao()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo", DataCriacao = DateTime.Now, Status = true });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var exDataCriacao = $"{DateTime.Now:yyyy-MM-dd}";
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() { Nome = "Nome" });
            var subcategorias = Assert.IsAssignableFrom<Subcategoria>(resposta.Value);
            //Assert
            Assert.Equal(exDataCriacao, $"{subcategorias.DataCriacao:yyyy-MM-dd}");
        }
        [Fact]
        public void Cadastro_Verifica_Status_True()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo", DataCriacao = DateTime.Now, Status = true });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() { Nome = "Nome" });            
            //Assert
            Assert.Equal(200, resposta.StatusCode);
        }
        [Fact]
        public void Cadastro_Verifica_Status_False()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo", DataCriacao = DateTime.Now, Status = false });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() { Nome = "Nome", Status = false });
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }
        [Fact]
        public void Cadastro_200_Ok()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo" });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() { Nome = "Nome" });
            var subcategorias = Assert.IsAssignableFrom<Subcategoria>(resposta.Value);
            //Assert
            Assert.Equal(200, resposta.StatusCode);
            Assert.NotNull(subcategorias);
        }
        [Fact]
        public void Cadastro_400_BadRequest()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns((Subcategoria)null);
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (BadRequestObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() { Nome = "Nome" });
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }
        [Fact]
        public void Cadastro_128Caracteres_200()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo" });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto() 
            {
                Nome = "aaaaaaaaaaaaaaaaaaaa" + // 20
                       "aaaaaaaaaaaaaaaaaaaa" + // 40
                       "aaaaaaaaaaaaaaaaaaaa" + // 60 
                       "aaaaaaaaaaaaaaaaaaaa" + // 80
                       "aaaaaaaaaaaaaaaaaaaa" + // 100
                       "aaaaaaaaaaaaaaaaaaaa" + // 120
                       "aaaaaaaa"
            });
            //Assert
            Assert.Equal(200, resposta.StatusCode);
        }
        [Fact]
        public void Cadastro_128Caracteres_400()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo" });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (StatusCodeResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaa" + // 20
                       "aaaaaaaaaaaaaaaaaaaa" + // 40
                       "aaaaaaaaaaaaaaaaaaaa" + // 60 
                       "aaaaaaaaaaaaaaaaaaaa" + // 80
                       "aaaaaaaaaaaaaaaaaaaa" + // 100
                       "aaaaaaaaaaaaaaaaaaaa" + // 120
                       "aaaaaaaaaaaaa"
            });            
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }
        [Fact]
        public void Cadastro_NullCaracteres_400()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo" });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (StatusCodeResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto()
            {
                Nome = ""
            });
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }
        [Fact]
        public void Cadastro_Categoria_Inativa_400()
        {
            //Arrange
            subcategoriaDao.Setup(x => x.CadastrarSubcategoria(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "NãoPodeSerNulo" });
            subcategoriaDao.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = false }); //Objeto de categoria
            var subcategoriaService = new SubcategoriaService(subcategoriaDao.Object, categoriaDao.Object, _mapper);
            var controller = new SubcategoriaController(produtoDao.Object, categoriaDao.Object, subcategoriaDao.Object, _mapper, subcategoriaService);
            //Act
            var resposta = (ObjectResult)controller.CadastrarSubcategorias(new CreateSubcategoriaDto()
            {
                Nome = "TesteCategoriaInativa"
            });
            //Assert
            Assert.Equal(400, resposta.StatusCode);
        }

    }
}
