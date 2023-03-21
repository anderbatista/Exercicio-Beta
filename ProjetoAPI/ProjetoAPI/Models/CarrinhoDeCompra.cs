using Org.BouncyCastle.Asn1.Mozilla;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Models
{
    public class CarrinhoDeCompra
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public double ValorTotalCarrinho { get; set; }
        public int QuantidadeTotalDeProdutos { get; set; }
        [Required]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        [Required]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        [JsonIgnore]
        public virtual List<ProdutoNoCarrinho> ProdutosNoCarrinho { get; set; }

        public void CalculaAdicionarValorTotalDoCarrinho()
        {
            /*
             * Calcula correto quando não está adicionado.
             * Caso o produto já exista o método calcula errado.
             */
            double valorTotal = 0;

            foreach (var produto in ProdutosNoCarrinho)
                valorTotal += produto.ValorUnitarioProduto;

            ValorTotalCarrinho = valorTotal;
        }

        //public void CalculaAdicionarValorTotalDoCarrinho()
        //{
        //    /*
        //     * Calcula correto quando está adicionado.
        //     */
        //
        //    double valorTotal = 0;

        //    foreach (var produto in ProdutosNoCarrinho)
        //        valorTotal += produto.ValorUnitarioProduto * produto.QuantidadeProduto;

        //    ValorTotalCarrinho = valorTotal;
        //}


        public void CalculaAdicionarQuantidadeProdutoNoCarrinho()
        {
            int qtdeCarrinho = 0;
            QuantidadeTotalDeProdutos = qtdeCarrinho;
        }


    }
}
