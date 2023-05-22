using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data.Dtos.ProdutoDto;
using ProjetoAPI.Services;
using System.Threading.Tasks;
using FluentResults;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoService _produtoService;
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduto(CreateProdutoDto produto)
        {
            try
            {
                Result resultado = await _produtoService.CadastrarProduto(produto);
                if (resultado.IsFailed) return BadRequest(resultado.Reasons);                
                return CreatedAtAction(nameof(PesquisaProdutos), new { Id = produto.Id }, produto);
            }
            catch
            {
                return BadRequest("SubcategoriaId ou CentroId Inválido.");
            }
        }
        [HttpPut("editar")]
        public async Task<IActionResult> EditarProduto(int id, UpdateProdutoDto produto)
        {
            if (await _produtoService.EditarProduto(produto) == 1)
            {
                return Ok("Editado com sucesso!");
            }
            return BadRequest("Falha ao editar...");

        }
        [HttpDelete("deletar")]
        public async Task<IActionResult> DeletaProduto(ReadProdutoDto produto)
        {
            if (await _produtoService.DeletarProduto(produto) == 1)
            {
                return Ok("Deletado com sucesso!");
            }
            return BadRequest("Falha ao deletar...");
        }
        [HttpGet("buscar")]
        public async Task<IActionResult> PesquisaProdutos(
            [FromQuery] int? id, [FromQuery] string nome, [FromQuery] string centroDeDistribuicao, [FromQuery] bool? status,
            [FromQuery] double? peso, [FromQuery] double? altura, [FromQuery] double? largura,
            [FromQuery] double? comprimento, [FromQuery] double? valor, [FromQuery] int? estoque,
            [FromQuery] string ordem, [FromQuery] int itensPg, [FromQuery] int pgAtual)
        {
            var result = await _produtoService.PesquisaPersonalizada(id, nome, centroDeDistribuicao, status, peso, altura, largura, comprimento, valor, estoque, ordem, itensPg, pgAtual);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
