using System;

namespace ProjetoAPI.Data.DTOs.CentroDistribuicaoDto
{
    public class UpdateCentroDistribuicaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; }
        public string Cep { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
