using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroUserService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EnderecoService _enderecoService;
        private ValidaUserService _validaUserService;
        private RoleManager<IdentityRole<int>> _roleManager;
        public CadastroUserService(IMapper mapper, EnderecoService enderecoService,
            ValidaUserService validaUserService, UserManager<CustomIdentityUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _enderecoService = enderecoService;
            _validaUserService = validaUserService;
            _roleManager = roleManager;
        }
        public async Task<Result> AddUserCliente(CreateUsuarioDto createDto)
        {
            var endereco = await _enderecoService.ConsultaEnderecoViaCep(createDto.Cep);
            if (endereco.Logradouro == null)
            {
                return Result.Fail("Não foi possível cadastrar endereço, verifique o CEP digitado.");
            }
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            usuario.Logradouro = endereco.Logradouro;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.Uf = endereco.Uf;
            var validaCpf = ValidaUserService.ValidaCPF(createDto.Cpf);

            if (validaCpf == false)
            {
                return Result.Fail("CPF inválido");
            }
            var validaDataNasc = ValidaUserService.ValidaDataNascimento(createDto.DataNascimento);
            if (validaDataNasc == false)
            {
                return Result.Fail("Data de nascimento inválida");
            }

            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            var usuarioRoleResul = _userManager.AddToRoleAsync(usuarioIdentity, "cliente").Result;

            try
            {
                if (resultadoIdentity.Succeeded)
                {
                    return Result.Ok();
                }
            }
            catch
            {
                return Result.Fail("CPF já cadastrado");
            }
            return Result.Fail("UserName ou E-mail já cadastrado.");
        }
        public async Task<Result> AddUserLojista(CreateUsuarioDto createDto)
        {
            var endereco = await _enderecoService.ConsultaEnderecoViaCep(createDto.Cep);
            if (endereco.Logradouro == null)
            {
                return Result.Fail("Não foi possível cadastrar endereço, verifique o CEP digitado.");
            }
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            usuario.Logradouro = endereco.Logradouro;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.Uf = endereco.Uf;
            var validaCpf = ValidaUserService.ValidaCPF(createDto.Cpf);

            if (validaCpf == false)
            {
                return Result.Fail("CPF inválido");
            }
            var validaDataNasc = ValidaUserService.ValidaDataNascimento(createDto.DataNascimento);
            if (validaDataNasc == false)
            {
                return Result.Fail("Data de nascimento inválida");
            }

            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            var usuarioRoleResul = _userManager.AddToRoleAsync(usuarioIdentity, "lojista").Result;

            try
            {
                if (resultadoIdentity.Succeeded)
                {
                    return Result.Ok();
                }
            }
            catch
            {
                return Result.Fail("CPF já cadastrado");
            }
            return Result.Fail("UserName ou E-mail já cadastrado.");
        }

    }
}
