using System;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.Dtos.ProdutoDto
{
    public class ReadProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int QtdeEstoque { get; set; }
        public int CentroId { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int SubcategoriaId { get; set; }

    }
}
