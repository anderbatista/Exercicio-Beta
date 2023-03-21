using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Services;

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
        public IActionResult PesquisaSubcategoria([FromQuery] ReadSubcategoriaDto readSub,
            [FromQuery] string ordem, [FromQuery] int itensPg, [FromQuery] int pgAtual)
        {
            var result = subcategoriaService.PesquisaPersonaizadaSubcategoria(readSub, ordem, itensPg, pgAtual);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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

