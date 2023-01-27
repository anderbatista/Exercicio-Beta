using System;

namespace UsuariosAPI.Data.Dtos
{
    public class ReadUsuarioDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
    }
}
