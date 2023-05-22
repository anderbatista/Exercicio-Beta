using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class ListaUsuarioService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EnderecoService _enderecoService;
        private ValidaUserService _validaUserService;
        public ListaUsuarioService(IMapper mapper, EnderecoService enderecoService,
            ValidaUserService validaUserService, UserManager<CustomIdentityUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _enderecoService = enderecoService;
            _validaUserService = validaUserService;
        }
        public async Task<List<ReadUsuarioDto>> ListarUsuario(string username, string cpf, string email, bool? status)
        {
            var usuario = await _userManager.Users.ToListAsync();
            List<ReadUsuarioDto> listaUser = new();
            foreach (var user in usuario)
            {
                var usuarioRetorno = _mapper.Map<ReadUsuarioDto>(user);
                listaUser.Add(usuarioRetorno);
            }
            if (username != null) 
            { 
                return listaUser.Where(user => user.UserName.ToLower().Contains(username.ToLower())).ToList(); 
            }
            if (cpf != null) 
            {
                return listaUser.Where(user => user.CPF == cpf).ToList();
            }
            if (email != null)
            { 
                return listaUser.Where(user => user.Email == email).ToList(); 
            }
            if (status != null) 
            {
                return listaUser.Where(user => user.Status == status).ToList();
            }
            return listaUser;
        }
    }
}
