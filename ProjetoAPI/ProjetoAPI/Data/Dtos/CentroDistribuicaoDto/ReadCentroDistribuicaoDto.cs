using ProjetoAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using ProjetoAPI.Data.Dtos.ProdutoDto;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.DTOs.CentroDistribuicaoDto
{
    public class ReadCentroDistribuicaoDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public bool? Status { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        [JsonIgnore]
        public virtual List<ReadProdutoDto> Produtos { get; set; }
        [JsonIgnore]
        public string Ordem { get; set; }
        [JsonIgnore]
        public int PgAtual { get; set; } = 1;
        [JsonIgnore]
        public int ItensPg { get; set; } = 20;
    }
}
