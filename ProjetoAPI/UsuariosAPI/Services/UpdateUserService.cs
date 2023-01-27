using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class UpdateUserService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EnderecoService _enderecoService;
        private ValidaUserService _validaUserService;
        private UserDbContext _userDbContext;
        public UpdateUserService(IMapper mapper, UserManager<CustomIdentityUser> userManager,
            EnderecoService enderecoService, ValidaUserService validaUserService, UserDbContext userDbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _enderecoService = enderecoService;
            _validaUserService = validaUserService;
            _userDbContext = userDbContext;
        }
        public async Task<Result> EditarUser(int id, UpdateUsuarioDto usuarioDto)
        {
            var usuario = _userManager.Users.FirstOrDefault(usuarioDto => usuarioDto.Id == id);
            if (usuario == null)
            {
                return Result.Fail("Usuário não encontrado");
            }
            if (!await ValidaCepAsync(usuarioDto.Cep))
            {
                return Result.Fail("CEP não encontrado");
            }            
            var endereco = await _enderecoService.ConsultaEnderecoViaCep(usuarioDto.Cep);            
            
            var usuarioAtualizado = _mapper.Map(usuarioDto, usuario);
            usuarioAtualizado.Cep = endereco.Cep;
            usuarioAtualizado.Bairro = endereco.Bairro;
            usuarioAtualizado.Logradouro = endereco.Logradouro;

            await _userManager.UpdateAsync(usuarioAtualizado);
            await _userDbContext.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<bool> ValidaCepAsync(string cep)
        {
            var endereco = await _enderecoService.ConsultaEnderecoViaCep(cep);

            if (endereco.Logradouro == null)
            {
                return false;
            }
            return true;
        }
    }
}
