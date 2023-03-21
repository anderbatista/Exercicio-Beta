using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Data.Interfaces
{
    public interface ICarrinhoDeCompraDao
    {
        public CarrinhoDeCompra CriaCarrinhoDeCompra(CarrinhoDeCompra createCarrinhoDeCompra);
        public CarrinhoDeCompra BuscaCarrinhoPorId(int? id);
        IReadOnlyList<CarrinhoDeCompra> ListaCarrinho();
        public void SalvaAlteracaoCarrinhoDeCompra();
        public CarrinhoDeCompra AtualizaTotaisDoCarrinho(CarrinhoDeCompra carrinho);
    }
}
