using AutoMapper;
using ProjetoAPI.Data.Dtos.CarrinhoDeCompra;
using ProjetoAPI.Data.Dtos.ProdutoNoCarrinhoDto;
using ProjetoAPI.Models;
using System.Linq;

namespace ProjetoAPI.Profiles
{
    public class CarrinhoDeCompraProfile : Profile
    {
        public CarrinhoDeCompraProfile()
        {
            CreateMap<CreateCarrinhoDeCompraDto, CarrinhoDeCompra>();
            CreateMap<CarrinhoDeCompra, ReadCarrinhoDeCompraDto>();
            CreateMap<UpdateCarrinhoDeCompraDto, CarrinhoDeCompra>();

            CreateMap<TotalProdutoNoCarrinhoDto, CarrinhoDeCompra>();
        }
    }
}
