using System;
using AutoMapper;
using Dapper;
using ProjetoAPI.Data.Dtos.ProdutoNoCarrinhoDto;
using ProjetoAPI.Models;
using ProjetoAPI.Services;
using ProjetoAPI.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjetoAPI.Data.DAO
{
    public class CarrinhoDeCompraDao : ICarrinhoDeCompraDao
    {
        private readonly IConfiguration _config;
        private AppDbContext _context;
        private IMapper _mapper;
        private ConsultaCepService consultaCepService;

        public CarrinhoDeCompraDao(IConfiguration config, AppDbContext context, IMapper mapper, ConsultaCepService consultaCepService)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
            this.consultaCepService = consultaCepService;
        }

        public CarrinhoDeCompra AtualizaTotaisDoCarrinho(CarrinhoDeCompra carrinho)
        {   
            var sql = " SELECT  P.IdCarrinho as CarrinhoId," +
                "               sum(P.QuantidadeProduto) as QuantidadeProdutos," +
                "               sum(P.ValorUnitarioProduto * P.QuantidadeProduto) as ValorTotalCarrinho" +
                "       FROM produtosnocarrinho P" +
                "       WHERE  P.IdCarrinho = @id" +
                "       GROUP BY  P.IdCarrinho";

            Console.WriteLine(sql);
            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                var result = conexao.Query<TotalProdutoNoCarrinhoDto>(sql.ToString(), carrinho).FirstOrDefault();

                var mapeamento = _mapper.Map<CarrinhoDeCompra>(result);
                if (mapeamento == null)
                {
                    return null;
                }
                mapeamento.QuantidadeTotalDeProdutos = result.QuantidadeProdutos;
                mapeamento.ValorTotalCarrinho = result.ValorTotalCarrinho;

                return mapeamento;
            }
        }

        public CarrinhoDeCompra CriaCarrinhoDeCompra(CarrinhoDeCompra createCarrinhoDeCompra)
        {
            _context.CarrinhoDeCompra.Add(createCarrinhoDeCompra);
            _context.SaveChanges();
            return createCarrinhoDeCompra;
        }

        public CarrinhoDeCompra BuscaCarrinhoPorId(int? id)
        {
            return _context.CarrinhoDeCompra.Where(c => c.Id == id).FirstOrDefault();
        }

        public IReadOnlyList<CarrinhoDeCompra> ListaCarrinho()
        {
            return _context.CarrinhoDeCompra.ToList();
        }

        public void SalvaAlteracaoCarrinhoDeCompra()
        {
            _context.SaveChanges();
        }

    }
}
