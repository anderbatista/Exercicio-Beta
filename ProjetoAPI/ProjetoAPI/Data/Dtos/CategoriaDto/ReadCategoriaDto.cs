using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Data.Dtos.CategoriaDto
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
