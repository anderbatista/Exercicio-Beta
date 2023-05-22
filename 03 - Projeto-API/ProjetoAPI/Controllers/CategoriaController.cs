using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Services;
using SerilogTimings;
using System.Collections.Generic;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private CategoriaService categoriaService;
        private readonly ILogger<CategoriaController> logger;

        public CategoriaController(CategoriaService categoriaService, ILogger<CategoriaController> logger)
        {
            this.categoriaService = categoriaService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult CadastrarCategorias([FromBody] CreateCategoriaDto categoriaDto)
        {
            logger.LogInformation($"#### POST - Inserção de uma nova categoria. ####");

            CreateCategoriaDto resultado;
            using (Operation.Time("#### Tempo de adição de uma nova Categoria."))
            {
                logger.LogInformation($"#### Requisição -> {JsonConvert.SerializeObject(categoriaDto)}. ####");
                resultado = categoriaService.AddCategoria(categoriaDto);                
            }
            if (resultado == null)
            {
                logger.LogWarning($"#### Erro: Inserção de dados. ");
                return BadRequest("Nome deve ser maior que 3 caracteres e menor que 128." +
                "\nImpossível cadastrar categoria com status inativo.");
            }
            return CreatedAtAction(nameof(PesquisaCategoria), new { nome = categoriaDto.Nome }, categoriaDto);
        }

        [HttpGet("buscar")]
        public IActionResult PesquisaCategoria([FromQuery] ReadCategoriaDto readCat,
            [FromQuery] string ordem, [FromQuery] int itensPg, [FromQuery] int pgAtual)
        {
            logger.LogInformation($"#### GET - Requisição de busca personalizada. ####");

            List<ReadCategoriaDto> resultado;
            using (Operation.Time("#### Tempo do método buscar Categoria."))
            {
                resultado = categoriaService.PesquisaCategoriaPersonalizada(readCat, ordem, itensPg, pgAtual);                
            }

            if (resultado.IsNullOrEmpty())
            {
                logger.LogWarning($"#### Erro: Categoria não localizada no banco de dados.");
                return NotFound("Nada encontrado.");
            }
            return Ok(resultado);
        }

        [HttpPut("editar/{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto novoNomeDto)
        {
            logger.LogInformation($"#### PUT - Requisição de editar categoria. ####");

            bool resultado;
            using (Operation.Time("#### Tempo de edição de Categoria."))
            {
                logger.LogInformation($"#### Requisição -> {JsonConvert.SerializeObject(novoNomeDto)}. ####");
                resultado = categoriaService.EditarCategoria(id, novoNomeDto);                
            }

            if (resultado == false)
            {
                logger.LogWarning($"#### Erro: Categoria informada não existe.");
                return BadRequest("Categoria não existe.");
            }
            return Ok("Editado com sucesso.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCategoria(int id)
        {
            logger.LogInformation($"#### DELETE - Requisição de excluir categoria. ####");

            bool resultado;
            using (Operation.Time("#### Tempo exclusão de Categoria."))
            {
                logger.LogInformation($"#### Requisição -> ID: {JsonConvert.SerializeObject(id)}. ####");
                resultado = categoriaService.DeletaCategoria(id);
            }

            if (resultado == false)
            {
                logger.LogWarning($"#### Erro: Categoria informada não existe no banco de dados.");
                return NotFound("Erro ao deletar.");
            }
            return Ok("Deletado com sucesso.");
        }
    }
}
