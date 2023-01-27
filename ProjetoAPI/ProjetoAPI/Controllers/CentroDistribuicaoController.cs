using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data.DTOs.CentroDistribuicaoDto;
using ProjetoAPI.Models;
using ProjetoAPI.Services;
using System;
using System.Threading.Tasks;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentroDistribuicaoController : ControllerBase
    {
        private CentroDistribuicaoService _centroService;
        public CentroDistribuicaoController(CentroDistribuicaoService centroService)
        {
            _centroService = centroService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCentro([FromBody] CreateCentroDistribuicaoDto centro)
        {
            Result resultado = await _centroService.CadastrarCentro(centro);
            if (resultado.IsFailed) return BadRequest(resultado.Reasons);
            return CreatedAtAction(nameof(PesquisaCentro), new { Id = centro.Id }, centro);
        }
        [HttpPut("editar")]
        public async Task<IActionResult> EditarCentro([FromBody] UpdateCentroDistribuicaoDto centro)
        {
            Result resultado = await _centroService.EditarCentro(centro);
            if (resultado.IsFailed) return BadRequest(resultado.Reasons);
            return Ok("Editado com sucesso!");
        }
        [HttpDelete("deletar")]
        public async Task<IActionResult> DeletarCentro(ReadCentroDistribuicaoDto centro)
        {
            if (await _centroService.DeletarCentro(centro) == 1)
            {
                return Ok("Deletado com sucesso!");
            }
            return BadRequest("Falha ao deletar...");
        }
        [HttpGet("buscar")]
        public async Task<IActionResult> PesquisaCentro([FromQuery] ReadCentroDistribuicaoDto readCd)
        {
            var result = await _centroService.PesquisaCentroPersonalizada(readCd);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
