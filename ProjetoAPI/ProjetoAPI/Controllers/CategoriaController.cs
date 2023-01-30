using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Models;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Data;
using ProjetoAPI.Services;
using FluentResults;
using System.Runtime.Intrinsics.Arm;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private CategoriaService categoriaService;
        public CategoriaController(CategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }
        [HttpPost]
        public IActionResult CadastrarCategorias([FromBody] CreateCategoriaDto categoriaDto)
        {
            var resultado = categoriaService.AddCategoria(categoriaDto);
            if (resultado == null) return BadRequest("Nome deve ser maior que 3 caracteres e menor que 128." +
                "\nImpossível cadastrar categoria com status inativo.");
            return CreatedAtAction(nameof(PesquisaCategoria), new { nome = categoriaDto.Nome }, categoriaDto);
        }
        [HttpGet("buscar")]
        public IActionResult PesquisaCategoria([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] string ordem)
        {
            var resultado = categoriaService.PesquisaCategoria(nome, status, ordem);
            if (resultado == null) return BadRequest("Favor digitar mais de 3 caracteres.");
            return Ok(resultado);
        }
        [HttpPut("editar/{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto novoNomeDto)
        {
            var resultado = categoriaService.EditarCategoria(id, novoNomeDto);
            if (resultado == false) return BadRequest("Categoria não existe.");
            return Ok("Editado com sucesso.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaCategoria(int id)
        {
            var resultado = categoriaService.DeletaCategoria(id);
            if (resultado == false) return NotFound("Erro ao deletar.");
            return Ok("Deletado com sucesso.");
        }
    }
}
