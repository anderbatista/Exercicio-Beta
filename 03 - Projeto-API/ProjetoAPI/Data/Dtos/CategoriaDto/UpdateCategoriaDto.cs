using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Data.Dtos.CategoriaDto
{
    public class UpdateCategoriaDto
    {
        [Required(ErrorMessage = "Nome da categoria é obrigatório.")]
        [RegularExpression("^[a-zA-Z' ']+$", ErrorMessage = "Não é permitido números e caracteres especiais.")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        public bool Status { get; set; }

    }
}
