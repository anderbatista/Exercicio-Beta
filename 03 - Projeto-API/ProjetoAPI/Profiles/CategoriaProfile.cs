using AutoMapper;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Models;

namespace ProjetoAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
            CreateMap<UpdateCategoriaDto, Categoria>();
        }
    }
}
