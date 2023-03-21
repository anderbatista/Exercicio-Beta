using ProjetoAPI.Data.Dtos.CarrinhoDeCompra;

namespace ProjetoAPI.Data.Dtos.CarrinhoProdutoDto
{
    public class CreateProdutoNoCarrinhoDto
    {
        public int IdCarrinho { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public double ValorUnitarioProduto { get; set; }
        public int QuantidadeProduto { get; set; }
    }
}
