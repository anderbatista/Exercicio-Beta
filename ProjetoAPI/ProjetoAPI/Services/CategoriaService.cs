using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Data.Repository;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Services
{
    public class CategoriaService
    {
        private ICategoriaDao categoriaDao;
        private ISubcategoriaDao subcategoriaDao;
        private IMapper _mapper;
        public CategoriaService(ICategoriaDao cDao, ISubcategoriaDao sdao, IMapper mapper)
        {
            categoriaDao = cDao;
            subcategoriaDao = sdao;
            _mapper = mapper;
        }
        public CreateCategoriaDto AddCategoria(CreateCategoriaDto categoriaDto)
        {
            if (categoriaDto.Nome.Length > 128 || categoriaDto.Nome.Length < 3 || categoriaDto.Nome == null 
                || categoriaDto.Nome == string.Empty || categoriaDto.Status == false)
            {
                return null;
            }
            categoriaDao.CadastrarCategoria(categoriaDto);
            return categoriaDto;
        }

        public List<ReadCategoriaDto>PesquisaCategoriaPersonalizada(ReadCategoriaDto readCat,
            string ordem, int itensPg, int pgAtual)
        {
            return categoriaDao.PesquisaCategoriaPersonalizada(readCat, ordem, itensPg, pgAtual);
        }

        public bool EditarCategoria(int id, [FromBody] UpdateCategoriaDto novoNomeDto)
        {
            var categoria = categoriaDao.BuscarCategoriasPorID(id);
            if (categoria == null)
            {
                return false;
            }
            if (novoNomeDto.Status != true)
            {
                var subcategorias = subcategoriaDao.ListaSubcategoriaPorIdcategoria(id);
                foreach (var subcategoria in subcategorias)
                {
                    subcategoria.Status = false;
                }
            }
            categoria.DataAlteracao = DateTime.Now;
            categoriaDao.FinalEditarCategoria(novoNomeDto, id);
            return true;
        }

        public bool DeletaCategoria(int id)
        {
            Categoria categoria = categoriaDao.BuscarCategoriasPorID(id);
            if (categoria == null)
            {
                return false;
            }
            categoriaDao.FinalDeletarCategoria(id);
            return true;
        }
    }
}
