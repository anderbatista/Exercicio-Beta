using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAPI.Services
{
    public class SubcategoriaService
    {
        private ISubcategoriaDao subcategoriaDao;
        private ICategoriaDao categoriaDao;
        private IProdutoDao produtoDao;
        private IMapper _mapper;
        private readonly ILogger<SubcategoriaService> logger;

        public SubcategoriaService(ISubcategoriaDao subcategoriaDao, ICategoriaDao dao, IMapper mapper, 
            IProdutoDao produtoDao, ILogger<SubcategoriaService> logger)
        {
            this.subcategoriaDao = subcategoriaDao;
            this.categoriaDao = dao;
            this.produtoDao = produtoDao;
            _mapper = mapper;
            this.logger = logger;
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
                logger.LogWarning($"#### Erro: Nome ou Status não atende aos crítérios.");
                return null;
            }
            logger.LogInformation($"#### Service - Inserção de uma nova subcategoria. ####");
            return subcategoriaDao.CadastrarSubcategoria(subcategoria);
        }

        public List<ReadSubcategoriaDto>PesquisaPersonaizadaSubcategoria(ReadSubcategoriaDto readSub, string ordem, int paginas, int itens)
        {
            logger.LogInformation($"#### Service - Buscando subcategoria. ####");
            return subcategoriaDao.PesquisaSubcategoriaPersonalizada(readSub, ordem, paginas, itens);   
        }

        public bool EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto novoNomeDto)
        {
            var subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            if (subcategoria == null)
            {
                logger.LogWarning($"#### Erro: Subcategoria não encontrada. Requisição -> {JsonConvert.SerializeObject(id)}. ####");
                return false;
            }
            var produtos = produtoDao.ListaProdutoPorIdSubcategoria(id);
            if (produtos.Count != 0 || subcategoria == null)
            {
                if(novoNomeDto.Status == true)
                {
                    return AuxEditarSubcategoria(id, novoNomeDto, subcategoria);
                }
                logger.LogWarning($"#### Erro: Impossível alterar status, contém produto associado.");
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
            logger.LogInformation($"#### Service - Editando subcategoria. ####");
            return true;
        }

        public bool DeletaSubategoria(int id)
        {
            Subcategoria subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            if (subcategoria == null)
            {
                logger.LogWarning($"#### Erro: Subategoria não encontrada.");
                return false;
            }
            subcategoriaDao.FinalDeletarSubcategoria(id);
            logger.LogInformation($"#### Service - Deletando subcategoria. ####");
            return true;
        }
    }
}
