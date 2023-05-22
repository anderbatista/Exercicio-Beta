using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAPI.Services
{
    public class CategoriaService
    {
        private ICategoriaDao categoriaDao;
        private ISubcategoriaDao subcategoriaDao;
        private IMapper _mapper;
        private readonly ILogger<CategoriaService> logger;

        public CategoriaService(ICategoriaDao cDao, ISubcategoriaDao sdao, IMapper mapper, ILogger<CategoriaService> logger)
        {
            categoriaDao = cDao;
            subcategoriaDao = sdao;
            _mapper = mapper;
            this.logger = logger;
        }

        public CreateCategoriaDto AddCategoria(CreateCategoriaDto categoriaDto)
        {
            if (categoriaDto.Nome.Length > 128 || categoriaDto.Nome.Length < 3 || categoriaDto.Nome == null 
                || categoriaDto.Nome == string.Empty || categoriaDto.Status == false)
            {
                logger.LogWarning($"#### Erro: Nome ou Status não atende aos crítérios.");
                return null;
            }
            logger.LogInformation($"#### Service - Inserção de uma nova categoria. ####");
            categoriaDao.CadastrarCategoria(categoriaDto);
            return categoriaDto;
        }

        public List<ReadCategoriaDto>PesquisaCategoriaPersonalizada(ReadCategoriaDto readCat,
            string ordem, int itensPg, int pgAtual)
        {
            logger.LogInformation($"#### Service - Buscando categoria. ####");
            return categoriaDao.PesquisaCategoriaPersonalizada(readCat, ordem, itensPg, pgAtual);
        }

        public bool EditarCategoria(int id, [FromBody] UpdateCategoriaDto novoNomeDto)
        {
            var categoria = categoriaDao.BuscarCategoriasPorID(id);
            if (categoria == null)
            {
                logger.LogWarning($"#### Erro: Categoria não encontrada. Requisição -> {JsonConvert.SerializeObject(id)}. ####");
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
            logger.LogInformation($"#### Service - Editando categoria. ####");
            return true;
        }

        public bool DeletaCategoria(int id)
        {
            Categoria categoria = categoriaDao.BuscarCategoriasPorID(id);
            if (categoria == null)
            {
                logger.LogWarning($"#### Erro: Categoria não encontrada.");
                return false;
            }
            categoriaDao.FinalDeletarCategoria(id);
            logger.LogInformation($"#### Service - Deletando categoria. ####");
            return true;
        }
    }
}
