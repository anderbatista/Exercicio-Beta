using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Models;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Data;
using ProjetoAPI.Services;
using FluentResults;
using ProjetoAPI.Data.Repository;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SubcategoriaController : ControllerBase
    {
        private SubcategoriaService subcategoriaService;
        private ISubcategoriaDao subcategoriaDao;
        private ICategoriaDao categoriaDao;
        private IProdutoDao produtoDao;
        private IMapper _mapper;
        public SubcategoriaController(IProdutoDao pdao, ICategoriaDao dao, ISubcategoriaDao sdao, IMapper mapper, SubcategoriaService subcategoriaService)
        {
            subcategoriaDao = sdao;
            produtoDao = pdao;
            categoriaDao = dao;
            _mapper = mapper;
            this.subcategoriaService = subcategoriaService;
        }
        [HttpPost]
        public IActionResult CadastrarSubcategorias([FromBody] CreateSubcategoriaDto subcategoriaDto)
        {
            var resultadoSubcategoria = subcategoriaService.AddSubcategoria(subcategoriaDto);
            if (resultadoSubcategoria == null) return BadRequest("Impossível criar subcategoria com status inativo, categoria não existe ou está inativada.");
            if (subcategoriaDto.Nome.Length > 128 || subcategoriaDto.Nome == string.Empty) return StatusCode(400);
            return Ok(resultadoSubcategoria);
        }
        [HttpGet("buscar")]
        public IActionResult PesquisaSubcategoria([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] string ordem, [FromQuery] int paginas, [FromQuery] int itens)
        {
            IQueryable<Subcategoria> lista = null;
            if (lista == null)
            {
                if (nome != null && nome.Length == 0 && nome.Length < 3)
                {
                    return BadRequest("Favor digitar mais de 3 caracteres.");
                }
                else if (nome != null && nome.Length >= 3)
                {
                    lista = subcategoriaDao.ListarSubcategoriasPorNome(nome);
                }
                else
                {
                    lista = subcategoriaDao.ListarSubcategorias();
                }
                if (status != null)
                {
                    lista = lista.Where(c => c.Status == status);
                }
                if (ordem != null)
                {
                    if (ordem == "za")
                    {
                        lista = lista.OrderByDescending(c => c.Nome);
                    }
                    if (ordem == "az")
                    {
                        lista = lista.OrderBy(c => c.Nome);
                    }
                }
                if (paginas != 0 && itens != 0)
                {
                    lista = lista.Skip((paginas - 1) * itens).Take(itens);
                }
                return Ok(lista);
            }
            else
            {
                return NotFound("Favor digitar um valor válido.");
            }
        }
        [HttpPut("editar/{id}")]
        public IActionResult EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto novoNomeDto)
        {
            var subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            var produtos = produtoDao.ListaProdutoPorIdSubcategoria(id);
            if (produtos.Count != 0)
            {
                return BadRequest("IMPOSSÍVEL ALTERAR STATUS:\n" +
                    $"{produtos.Count} produto(s) atrelado(s) ");

                // quando nulo da excessão... Preciso rever...
            }
            if (subcategoria == null)
            {
                return NotFound();
            }
            else
            {
                if (subcategoria.Status != true)
                {
                    subcategoria.Status = false;
                }
                subcategoria.DataAlteracao = DateTime.Now;
                subcategoriaDao.FinalEditarSubcategoria(id, novoNomeDto);
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaSubategoria(int id)
        {
            Subcategoria subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            if (subcategoria == null)
            {
                return NotFound();
            }
            subcategoriaDao.FinalDeletarSubcategoria(id);
            return NoContent();
        }
    }
}
