using System;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProjetoAPI.Data.Dtos.SubcategoriaDto
{
    public class ReadSubcategoriaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Categoria Categoria { get; set; }
    }
}
