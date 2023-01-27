using AutoMapper;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Models;

namespace ProjetoAPI.Profiles
{
    public class SubcategoriaProfile : Profile
    {
        public SubcategoriaProfile()
        {
            CreateMap<CreateSubcategoriaDto, Subcategoria>();
            CreateMap<Subcategoria, ReadSubcategoriaDto>();
            CreateMap<UpdateSubcategoriaDto, Subcategoria>();
        }
        
    }
}
