using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.Dtos.CategoriaDto
{
    public class CreateCategoriaDto
    {
        [Required(ErrorMessage = "Nome da categoria é obrigatório.")]
        [RegularExpression("^[a-zA-Z' ']+$", ErrorMessage = "Não é permitido números e caracteres especiais.")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; } = DateTime.Now;
        public bool? Status { get; set; } = true;
    }
}
