using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Models;
using ProjetoAPI.Services;
using SerilogTimings;
using System.Collections.Generic;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SubcategoriaController : ControllerBase
    {
        private SubcategoriaService subcategoriaService;
        private readonly ILogger<SubcategoriaController> logger;
        public SubcategoriaController(SubcategoriaService subcategoriaService, ILogger<SubcategoriaController> logger)
        {
            this.subcategoriaService = subcategoriaService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult CadastrarSubcategorias([FromBody] CreateSubcategoriaDto subcategoriaDto)
        {
            logger.LogInformation($"#### POST - Inserção de uma nova subcategoria. ####");

            Subcategoria resultadoSubcategoria;
            using (Operation.Time("#### Tempo de adição de uma nova Subcategoria."))
            {
                logger.LogInformation($"#### Requisição -> {JsonConvert.SerializeObject(subcategoriaDto)}. ####");
                resultadoSubcategoria = subcategoriaService.AddSubcategoria(subcategoriaDto);
            }

            if (resultadoSubcategoria == null) 
            {
                logger.LogWarning($"#### Erro: Inserção de dados. ");
                return BadRequest("Impossível criar subcategoria com status inativo, categoria não existe ou está inativada.");
            }

            if (subcategoriaDto.Nome.Length > 128 || subcategoriaDto.Nome == string.Empty) 
            {
                logger.LogWarning($"#### Erro: Inserção de dados. ");
                return StatusCode(400);
            } 

            return Ok(resultadoSubcategoria);
        }

        [HttpGet("buscar")]
        public IActionResult PesquisaSubcategoria([FromQuery] ReadSubcategoriaDto readSub,
            [FromQuery] string ordem, [FromQuery] int itensPg, [FromQuery] int pgAtual)
        {
            logger.LogInformation($"#### GET - Requisição de busca personalizada. ####");

            List<ReadSubcategoriaDto> result;
            using (Operation.Time("#### Tempo do método buscar Subcategoria."))
            {
                result = subcategoriaService.PesquisaPersonaizadaSubcategoria(readSub, ordem, itensPg, pgAtual);
            }                
            if (result == null)
            {
                logger.LogWarning($"#### Erro: Subcategoria não localizada no banco de dados.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("editar/{id}")]
        public IActionResult EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto novoNomeDto)
        {
            logger.LogInformation($"#### PUT - Requisição de editar subcategoria. ####");

            bool resultado;
            using (Operation.Time("#### Tempo de edição de Subcategoria."))
            {
                logger.LogInformation($"#### Requisição -> {JsonConvert.SerializeObject(novoNomeDto)}. ####");
                resultado = subcategoriaService.EditarSubcategoria(id, novoNomeDto);
            }

            if (resultado == false) 
            {
                logger.LogWarning($"#### Erro: Subcategoria informada não existe.\nOr: Não possível alterar status de subcategoria com produtos associados.");
                return BadRequest("Impossível alterar.\nSubcategoria não localizada ou existe produtos associados.");
            } 
            return Ok("Editado com sucesso.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaSubategoria(int id)
        {
            logger.LogInformation($"#### DELETE - Requisição de excluir subcategoria. ####");

            bool resultado;
            using (Operation.Time("#### Tempo exclusão de Subcategoria."))
            {
                logger.LogInformation($"#### Requisição -> ID: {JsonConvert.SerializeObject(id)}. ####");
                resultado = subcategoriaService.DeletaSubategoria(id);
            }

            if (resultado == false)
            {
                logger.LogWarning($"#### Erro: Subcategoria informada não existe no banco de dados.");
                return NotFound("Erro ao deletar.");
            } 
            return Ok("Deletado com sucesso.");
        }
    }
}

