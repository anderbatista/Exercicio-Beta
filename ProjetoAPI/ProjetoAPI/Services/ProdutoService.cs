using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.ProdutoDto;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjetoAPI.Services
{
    public class ProdutoService
    {
        private IMapper _mapper;
        private ISubcategoriaDao subcategoriaDao;
        private ICategoriaDao categoriaDao;
        private IProdutoDao produtoDao;

        public ProdutoService(ICategoriaDao dao, ISubcategoriaDao sdao, IProdutoDao pdao, IMapper mapper)
        {
            _mapper = mapper;
            subcategoriaDao = sdao;
            categoriaDao = dao;
            produtoDao = pdao;
        }
        public async Task<Result> CadastrarProduto(CreateProdutoDto produto)
        {
            if (produto.Status == false)
            {
                return Result.Fail("Impossível cadastrar produdto com status inativo.");
            }
            await produtoDao.AddProduto(produto);
            return Result.Ok();
        }
        public async Task<int> EditarProduto(UpdateProdutoDto produto)
        {
            return await produtoDao.EditarProduto(produto);
        }
        public async Task<int> DeletarProduto(ReadProdutoDto produto)
        {
            return await produtoDao.DeletarProduto(produto);
        }
        public async Task<List<ReadProdutoDto>> PesquisaPersonalizada(int? id, string nome, string centroDeDistribuicao, bool? status,
            double? peso, double? altura, double? largura,
            double? comprimento, double? valor, int? estoque,
            string ordem, int itensPg, int pgAtual)
        {
            return await produtoDao.PesquisaPersonalizada(id, nome, centroDeDistribuicao, status, peso, altura, largura, comprimento, valor, estoque, ordem, itensPg, pgAtual);
        }
    }
}
