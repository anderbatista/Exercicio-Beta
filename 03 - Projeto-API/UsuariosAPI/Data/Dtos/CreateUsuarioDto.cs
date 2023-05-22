using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UsuariosAPI.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required] public string UserName { get; set; }
        [Required] public DateTime DataNascimento { get; set; }
        [Required] public string Cpf { get; set; }
        [Required] public string Cep { get; set; }
        [Required] public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required] public string Email { get; set; }
        [Required] [DataType(DataType.Password)] public string Password { get; set; }
        [Required] [Compare("Password", ErrorMessage = "Verifique a senha digitada")] public string RePassword { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DataModificacao { get; set; } = null;
    }
}