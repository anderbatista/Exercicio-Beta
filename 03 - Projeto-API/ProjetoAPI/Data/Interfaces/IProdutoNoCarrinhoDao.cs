using ProjetoAPI.Data.Dtos.CarrinhoProdutoDto;
using ProjetoAPI.Models;

namespace ProjetoAPI.Data.Interfaces
{
    public interface IProdutoNoCarrinhoDao
    {
        public CreateProdutoNoCarrinhoDto SalvaProdutoNoCarrinho(CreateProdutoNoCarrinhoDto criaRelacao);
        public ProdutoNoCarrinho BuscaItemNoCarrinho(CreateProdutoNoCarrinhoDto buscaProdutoNoCarrinho);
        public ProdutoNoCarrinho BuscaProdutoNoCarrinhoPorId(int id);
        public void DeletaProdutoNoCarrinho(int id);
    }
}
