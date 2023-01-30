using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Data.Repository;
using ProjetoAPI.Models;
using System;
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
        public IQueryable PesquisaPersonaizadaSubcategoria([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] string ordem, [FromQuery] int paginas, [FromQuery] int itens)
        {
            IQueryable<Subcategoria> lista = null;
            if (lista == null)
            {
                if (nome != null && nome.Length < 3)
                {
                    return null;
                }
                else if (nome != null && nome.Length >= 3)
                {
                    lista = subcategoriaDao.ListarSubcategoriasPorNome(nome);
                }
                else
                {
                    lista = subcategoriaDao.ListarSubcategorias();
                }
                if (status != null)
                {
                    lista = lista.Where(c => c.Status == status);
                }
                if (ordem != null)
                {
                    if (ordem == "za")
                    {
                        lista = lista.OrderByDescending(c => c.Nome);
                    }
                    if (ordem == "az")
                    {
                        lista = lista.OrderBy(c => c.Nome);
                    }
                }
                if (paginas != 0 && itens != 0)
                {
                    lista = lista.Skip((paginas - 1) * itens).Take(itens);
                }
                return lista;
            }
            else
            {
                return null;
            }
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
