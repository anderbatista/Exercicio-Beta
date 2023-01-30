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
        public SubcategoriaController(SubcategoriaService subcategoriaService)
        {
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
            var resultado = subcategoriaService.PesquisaPersonaizadaSubcategoria(nome, status, ordem, paginas, itens);
            if (resultado == null) return BadRequest("Favor digitar mais de 3 caracteres.");
            return Ok(resultado);
        }
        [HttpPut("editar/{id}")]
        public IActionResult EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto novoNomeDto)
        {
            var resultado = subcategoriaService.EditarSubcategoria(id, novoNomeDto);
            if (resultado == false) return BadRequest("Impossível alterar.");
            return Ok("Editado com sucesso.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaSubategoria(int id)
        {
            var resultado = subcategoriaService.DeletaSubategoria(id);
            if (resultado == false) return NotFound("Erro ao deletar.");
            return Ok("Deletado com sucesso.");
        }
    }
}

