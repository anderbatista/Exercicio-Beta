using System.ComponentModel.DataAnnotations;
using System;

namespace UsuariosAPI.Data.Dtos
{
    public class UpdateUsuarioDto
    {
        [StringLength(250, ErrorMessage = "Máximo 250 caracteres.")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public DateTime? DataModificacao { get; set; } = DateTime.Now;
    }
}
