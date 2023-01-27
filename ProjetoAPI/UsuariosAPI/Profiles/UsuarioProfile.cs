using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            CreateMap<ReadUsuarioDto, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, ReadUsuarioDto>();
            CreateMap<UpdateUsuarioDto, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, UpdateUsuarioDto>();
        }
    }
}