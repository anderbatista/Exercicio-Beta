using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Data.Repository;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoAPI.Services
{
    public class SubcategoriaService
    {
        private ISubcategoriaDao subcategoriaDao;
        private ICategoriaDao categoriaDao;
        private IProdutoDao produtoDao;
        private IMapper _mapper;

        public SubcategoriaService(ISubcategoriaDao subcategoriaDao, ICategoriaDao dao, IMapper mapper, IProdutoDao produtoDao)
        {
            this.subcategoriaDao = subcategoriaDao;
            this.categoriaDao = dao;
            this.produtoDao = produtoDao;
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

        public List<ReadSubcategoriaDto>PesquisaPersonaizadaSubcategoria(ReadSubcategoriaDto readSub, string ordem, int paginas, int itens)
        {
            return subcategoriaDao.PesquisaSubcategoriaPersonalizada(readSub, ordem, paginas, itens);   
        }

        public bool EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto novoNomeDto)
        {
            var subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            var produtos = produtoDao.ListaProdutoPorIdSubcategoria(id);
            if (produtos.Count != 0 || subcategoria == null)
            {
                if(novoNomeDto.Status == true)
                {
                    return AuxEditarSubcategoria(id, novoNomeDto, subcategoria);
                }
                return false; 
            }
            else
            {
                if (subcategoria.Status != true)
                {
                    subcategoria.Status = false;
                }
                return AuxEditarSubcategoria(id, novoNomeDto, subcategoria);
            }
        }

        private bool AuxEditarSubcategoria(int id, UpdateSubcategoriaDto novoNomeDto, Subcategoria subcategoria)
        {
            subcategoria.DataAlteracao = DateTime.Now;
            subcategoriaDao.FinalEditarSubcategoria(id, novoNomeDto);
            return true;
        }

        public bool DeletaSubategoria(int id)
        {
            Subcategoria subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            if (subcategoria == null)
            {
                return false;
            }
            subcategoriaDao.FinalDeletarSubcategoria(id);
            return true;
        }
    }
}
