using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
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

    //public void ValidaCreateCategoriaDto(CreateCategoriaDto categoriaDto)
    //{
    //    CreateCategoriaDto categoria = new CreateCategoriaDto();
    //    categoria.Nome = categoriaDto.Nome;

    //    //cria a instância de ValidationContext
    //    var ctx = new ValidationContext(categoriaDto);

    //    // Criando uma lista para tratar o resultado da validação
    //    var resultados = new List<ValidationResult>();

    //    if (!Validator.TryValidateObject(categoriaDto, ctx, resultados, true))
    //    {
    //        foreach (var erros in resultados)
    //        {
    //            Log.Error($"Erro de validação : {erros}");
    //        }
    //    }
    //}

}
