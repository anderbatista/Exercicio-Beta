using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da categoria é obrigatório.")]
        [RegularExpression("^[a-zA-Z' ']+$", ErrorMessage = "Não é permitido números e caracteres especiais.")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres.")]
        public string Nome { get; set; }
        public bool? Status { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        [JsonIgnore]
        public virtual List<Subcategoria> Subcategorias { get; set; }
    }
}
