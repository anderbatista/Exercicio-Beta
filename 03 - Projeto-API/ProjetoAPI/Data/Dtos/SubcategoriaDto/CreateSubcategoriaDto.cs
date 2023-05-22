using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.Dtos.SubcategoriaDto
{
    public class CreateSubcategoriaDto
    {
        [Required(ErrorMessage = "Nome da subcategoria é obrigatório.")]
        [RegularExpression("^[a-zA-Z' ']+$", ErrorMessage = "Não é permitido números e caracteres especiais.")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        public bool? Status { get; set; } = true;
    }
}
