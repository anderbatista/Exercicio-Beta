using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Models
{
    public class ProdutoNoCarrinho
    {
        [Key]
        public int Id { get; set; }
        public int IdCarrinho { get; set; }
        public virtual CarrinhoDeCompra CarrinhoDeCompra { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public double ValorUnitarioProduto { get; set; }
        public int QuantidadeProduto { get; set; }
    }
}
