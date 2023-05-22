using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.DTOs.CentroDistribuicaoDto
{
    public class UpdateCentroDistribuicaoDto
    {
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [JsonIgnore]
        public string Bairro { get; set; }
        [JsonIgnore]
        public string Localidade { get; set; }
        [JsonIgnore]
        public string UF { get; set; }
        public string Cep { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
