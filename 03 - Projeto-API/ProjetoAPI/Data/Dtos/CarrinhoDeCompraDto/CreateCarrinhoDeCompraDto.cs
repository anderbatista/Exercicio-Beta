using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Data.Dtos.CarrinhoDeCompra
{
    public class CreateCarrinhoDeCompraDto
    {
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
