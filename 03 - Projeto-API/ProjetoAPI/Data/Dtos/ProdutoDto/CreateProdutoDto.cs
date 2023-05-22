using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.Dtos.ProdutoDto
{
    public class CreateProdutoDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do produto é obrigatório.")]
        [RegularExpression("^[a-zA-Z0-9' ']+$")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9' ']+$")]
        [StringLength(512, ErrorMessage = "Quantidade máxima de 512 caracteres.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Peso obrigatório.")]
        public double Peso { get; set; }
        [Required(ErrorMessage = "Altura obrigatório.")]
        public double Altura { get; set; }
        [Required(ErrorMessage = "largura obrigatório.")]
        public double Largura { get; set; }
        [Required(ErrorMessage = "Comprimento obrigatório.")]
        public double Comprimento { get; set; }
        [Required(ErrorMessage = "Valor obrigatório.")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "Quantidade em estoque obrigatório.")]
        public int QtdeEstoque { get; set; }
        [Required(ErrorMessage = "Centro de distribuição obrigatório.")]
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; } = null;
        [JsonIgnore]
        public Subcategoria Subcategoria { get; set; }
        [Required(ErrorMessage = "ID subcategoria obrigatório.")]
        public int SubcategoriaId { get; set; }
        [JsonIgnore]
        public virtual CentroDistribuicao CentroDeDistribuicao { get; set; }
        [Required(ErrorMessage = "ID centro de distribuição obrigatório.")]
        public int CentroId { get; set; }
    }
}
