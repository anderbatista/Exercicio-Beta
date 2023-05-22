using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Models
{
    public class Produto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do produto é obrigatório.")]
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
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
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        [JsonIgnore]
        public virtual Subcategoria Subcategoria { get; set; }
        public int SubcategoriaId { get; set; }
        [JsonIgnore]
        public virtual CentroDistribuicao CentroDeDistribuicao { get; set; }
        public int CentroId { get; set; }
    }
}
