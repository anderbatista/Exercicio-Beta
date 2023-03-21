using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Services;

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
        public IActionResult PesquisaCategoria([FromQuery] ReadCategoriaDto readCat,
            [FromQuery] string ordem, [FromQuery] int itensPg, [FromQuery] int pgAtual)
        {
            var result = categoriaService.PesquisaCategoriaPersonalizada(readCat, ordem, itensPg, pgAtual);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
