using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Services
{
    public class CategoriaService
    {
        private ICategoriaDao categoriaDao;
        private IMapper _mapper;
        public CategoriaService(ICategoriaDao categoriaDao, IMapper mapper)
        {
            this.categoriaDao = categoriaDao;
            _mapper = mapper;
        }
        public CreateCategoriaDto AddCategoria(CreateCategoriaDto categoriaDto)
        {
            categoriaDao.CadastrarCategoria(categoriaDto);
            return categoriaDto;
        }
    }
}
