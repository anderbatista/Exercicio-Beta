using AutoMapper;
using ProjetoAPI.Data.DTOs.CentroDistribuicaoDto;
using ProjetoAPI.Models;

namespace ProjetoAPI.Profiles
{
    public class CentroDistribuicaoProfile : Profile
    {
        public CentroDistribuicaoProfile()
        {
            CreateMap<CreateCentroDistribuicaoDto, CentroDistribuicao>();
            CreateMap<CentroDistribuicao, ReadCentroDistribuicaoDto>();
            CreateMap<UpdateCentroDistribuicaoDto, CentroDistribuicao>();
            CreateMap<ReadCentroDistribuicaoDto, CentroDistribuicao>();
        }
    }
}
