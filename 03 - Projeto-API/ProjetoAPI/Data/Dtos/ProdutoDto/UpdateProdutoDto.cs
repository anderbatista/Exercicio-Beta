using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Data.Dtos.ProdutoDto
{
    public class UpdateProdutoDto
    {
        public int Id { get; set; }
        [RegularExpression("^[a-zA-Z0-9' ']+$")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        [RegularExpression("^[a-zA-Z0-9' ']+$")]
        [StringLength(512, ErrorMessage = "Quantidade máxima de 512 caracteres.")]
        public string Descricao { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int QtdeEstoque { get; set; }
        public int CentroId { get; set; }
        public bool Status { get; set; }
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
