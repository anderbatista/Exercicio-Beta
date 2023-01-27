using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(250, ErrorMessage = "Máximo 250 caracteres.")]
        public string UserName { get; set; }
        public DateTime DataNascimento { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }       
        public string CPF { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
    }
}