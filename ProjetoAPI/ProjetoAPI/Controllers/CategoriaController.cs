using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Models;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Data;
using ProjetoAPI.Services;
using FluentResults;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private ICategoriaDao categoriaDao;
        private ISubcategoriaDao subcategoriaDao;
        private CategoriaService categoriaService;
        public CategoriaController(ICategoriaDao dao, ISubcategoriaDao sdao, CategoriaService categoriaService)
        {
            categoriaDao = dao;
            subcategoriaDao = sdao;

            this.categoriaService = categoriaService;
        }
        [HttpPost]
        public IActionResult CadastrarCategorias([FromBody] CreateCategoriaDto categoriaDto)
        {
            if (categoriaDto.Nome == null || categoriaDto.Nome == string.Empty || categoriaDto.Status == false)
            {
                return NotFound("Impossível cadastrar categoria com status inativo.");
            }
            if (categoriaDto.Nome.Length > 128) return StatusCode(400);
            return CreatedAtAction(nameof(PesquisaCategoria), new { nome = categoriaDto.Nome }, categoriaDto);
        }
        [HttpGet("buscar")]
        public IActionResult PesquisaCategoria([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] string ordem)
        {
            IQueryable<Categoria> lista = null;
            if (lista == null)
            {
                if (nome != null && nome.Length == 0 && nome.Length < 3)
                {
                    return BadRequest("Favor digitar mais de 3 caracteres.");
                }
                if (nome != null && nome.Length >= 3)
                {
                    lista = categoriaDao.ListarCategoriasPorNome(nome);
                }
                else
                {
                    lista = categoriaDao.ListarCategorias();
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
                    else if (ordem == "az")
                    {
                        lista = lista.OrderBy(c => c.Nome);
                    }
                }
                return Ok(lista);
            }
            else
            {
                return NotFound("Favor digitar um valor válido.");
            }
        }
        [HttpPut("editar/{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto novoNomeDto)
        {
            // Não consegui criar a validação para não mudar o status quando tem
            // produto vinculado. 
            var categoria = categoriaDao.BuscarCategoriasPorID(id);
            if (categoria == null)
            {
                return NotFound("Categoria não existe.");
            }
            if (novoNomeDto.Status != true)
            {
                var subcategorias = subcategoriaDao.ListaSubcategoriaPorIdcategoria(id);
                foreach (var subcategoria in subcategorias)
                {
                    subcategoria.Status = false;
                }
            }
            categoria.DataAlteracao = DateTime.Now;
            categoriaDao.FinalEditarCategoria(novoNomeDto, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaCategoria(int id)
        {
            Categoria categoria = categoriaDao.BuscarCategoriasPorID(id);
            if (categoria == null)
            {
                return NotFound();
            }
            categoriaDao.FinalDeletarCategoria(id);
            return NoContent();
        }
    }
}
