using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Models
{
    public class CentroDistribuicao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }        
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(256, ErrorMessage = "Quantidade máxima de 256 caracteres.")]
        public string Logradouro { get; set; }
        [Required]
        public int Numero { get; set; }        
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Complemento { get; set; }        
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Bairro { get; set; }        
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }
    }
}
