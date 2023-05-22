using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Data.DTOs.CentroDistribuicaoDto
{
    public class CreateCentroDistribuicaoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = " Campo nome é obrigatório")]
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        [RegularExpression("^[a-zA-Z0-9' ']+$", ErrorMessage = "Não é permitido caracteres especiais")]
        [StringLength(256, ErrorMessage = "Quantidade máxima de 256 caracteres.")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Campo número é obrigatório")]
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
        [Required(ErrorMessage = "Campo cep é obrigatório")]
        public string Cep { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; } = null;
    }
}
