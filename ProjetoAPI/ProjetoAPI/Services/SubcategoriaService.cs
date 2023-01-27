using AutoMapper;
using FluentResults;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Data.Repository;
using ProjetoAPI.Models;
using System;

namespace ProjetoAPI.Services
{
    public class SubcategoriaService
    {
        private ISubcategoriaDao subcategoriaDao;
        private ICategoriaDao categoriaDao;
        private IMapper _mapper;

        public SubcategoriaService(ISubcategoriaDao subcategoriaDao, ICategoriaDao dao, IMapper mapper)
        {
            this.subcategoriaDao = subcategoriaDao;
            this.categoriaDao = dao;
            _mapper = mapper;
        }
        public Subcategoria AddSubcategoria(CreateSubcategoriaDto subcategoriaDto)
        {
            Subcategoria subcategoria = _mapper.Map<Subcategoria>(subcategoriaDto);
            subcategoria.DataCriacao = DateTime.Now;
            subcategoria.DataAlteracao = null;
            var categoria = subcategoriaDao.BuscaCategoriaPorId(subcategoria);
            if (categoria == null) return null;
            if (categoria.Status != true || subcategoria.Status != true)
            {
                return null;
            }
            return subcategoriaDao.CadastrarSubcategoria(subcategoria);
        }
    }
}
