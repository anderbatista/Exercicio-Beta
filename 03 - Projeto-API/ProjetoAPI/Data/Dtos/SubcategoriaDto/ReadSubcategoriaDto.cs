using System;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProjetoAPI.Data.Dtos.SubcategoriaDto
{
    public class ReadSubcategoriaDto
    {       
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Status { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int CategoriaId { get; set; }
    }
}
