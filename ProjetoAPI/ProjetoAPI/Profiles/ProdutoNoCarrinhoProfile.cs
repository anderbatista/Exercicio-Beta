using AutoMapper;
using ProjetoAPI.Data.Dtos.CarrinhoProdutoDto;
using ProjetoAPI.Models;

namespace ProjetoAPI.Profiles
{
    public class ProdutoNoCarrinhoProfile : Profile
    {
        public ProdutoNoCarrinhoProfile() 
        {
            CreateMap<CreateProdutoNoCarrinhoDto, ProdutoNoCarrinho>();
            CreateMap<ProdutoNoCarrinho, ReadProdutoNoCarrinhoDto>();
            CreateMap<UpdateProdutoNoCarrinhoDto, ProdutoNoCarrinho>();

            CreateMap<ProdutoNoCarrinho, CreateProdutoNoCarrinhoDto>();
        }
    }
}
