using AutoMapper;
using ProjetoAPI.Data.Dtos.ProdutoDto;
using ProjetoAPI.Models;

namespace ProjetoAPI.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap<Produto, ReadProdutoDto>();
            CreateMap<UpdateProdutoDto, Produto>();
        }

    }
}
