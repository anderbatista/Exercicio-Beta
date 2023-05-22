using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Data.Dtos.ConsultaCepDto
{
    public class ReadConsultaCepDto
    {
        [Required]
        public string Cep { get; set; }
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Localidade { get; set; }
        [Required]
        public string UF { get; set; }
    }
}
