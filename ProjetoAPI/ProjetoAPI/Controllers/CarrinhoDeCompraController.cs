using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data.Dtos.CarrinhoDeCompra;
using ProjetoAPI.Data.Dtos.CarrinhoProdutoDto;
using ProjetoAPI.Models;
using ProjetoAPI.Services;
using System.Threading.Tasks;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoDeCompraController : ControllerBase
    {
        private CarrinhoDeCompraService carrinhoCompraService;

        public CarrinhoDeCompraController(CarrinhoDeCompraService carrinhoCompraService)
        {
            this.carrinhoCompraService = carrinhoCompraService;
        }

        [HttpPost]
        public async Task<CarrinhoDeCompra> CriarCarrinhoDeCompra([FromBody] CreateCarrinhoDeCompraDto carrinhoDeCompraDto)
        {
            return await carrinhoCompraService.CriarCarrinho(carrinhoDeCompraDto);
        }

        [HttpPut("adicionarProduto")]
        public IActionResult AdicionarProduto(CreateProdutoNoCarrinhoDto alteraDto)
        {
            var resultado = carrinhoCompraService.AdicionarProduto(alteraDto);
            if (resultado == null) return BadRequest();
            return Ok(resultado);
        }

        [HttpPut("removerProduto")]
        public IActionResult RemoverProduto(CreateProdutoNoCarrinhoDto alteraDto)
        {
            var resultado = carrinhoCompraService.RemoverProduto(alteraDto);
            if (resultado == null) return BadRequest();
            return Ok(resultado);
        }

        [HttpGet("buscaCarrinhoDeCompra")]
        public IActionResult ListarCarrinho([FromQuery]int? id)
        {
            if(id != null)
            {
                return Ok(carrinhoCompraService.ListarCarrinhoPorId(id));
            }
            return Ok(carrinhoCompraService.ListarCarrinho());
        }

    }
}
