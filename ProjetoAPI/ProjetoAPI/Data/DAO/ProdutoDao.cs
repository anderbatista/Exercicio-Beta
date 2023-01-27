using System;
using AutoMapper;
using ProjetoAPI.Data.Dtos.ProdutoDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;

namespace ProjetoAPI.Data.Repository
{
    public class ProdutoDao : IProdutoDao
    {
        private readonly IConfiguration _config;
        private AppDbContext _context;
        private IMapper _mapper;
        private ISubcategoriaDao subcategoriaDao;
        public ProdutoDao(IConfiguration configuration, ISubcategoriaDao sdao, AppDbContext context, IMapper mapper)
        {
            _config = configuration;
            _context = context;
            _mapper = mapper;
            subcategoriaDao = sdao;
        }
        public async Task<CreateProdutoDto> AddProduto(CreateProdutoDto produto)
        {
            var sql = "INSERT INTO Produtos (Nome, Descricao, Peso, Altura, Largura, Comprimento, Valor, QtdeEstoque, Status, DataCriacao, SubcategoriaId, CentroId)" +
                "VALUES(@Nome, @Descricao, @Peso, @Altura, @Largura, @Comprimento, @Valor, @QtdeEstoque, @Status, @DataCriacao, @SubcategoriaId, @CentroId);" +
                "SELECT LAST_INSERT_ID();";

            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                produto.Id = await conexao.QuerySingleAsync<int>(sql, produto);
                return produto;
            }
        }
        public async Task<int> EditarProduto(UpdateProdutoDto produto)
        {
            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                var editar = "UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Peso = @Peso, Altura = @Altura, Largura = @Largura, " +
                             "Comprimento = @Comprimento, Valor = @Valor, QtdeEstoque = @QtdeEstoque, CentroId = @CentroId, " +
                             "Status = @Status, DataAlteracao = @DataAlteracao WHERE Id = @Id";

                var result = await conexao.ExecuteAsync(editar, produto);
                return result;
            }
        }
        public async Task<int> DeletarProduto(ReadProdutoDto produto)
        {
            var sql = "DELETE FROM Produtos WHERE Id = @Id";
            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                var result = await conexao.ExecuteAsync(sql, produto);
                return result;
            }
        }
        public async Task<List<ReadProdutoDto>> PesquisaPersonalizada(int? id, string nome, string centroDeDistribuicao, bool? status,
            double? peso, double? altura, double? largura,
            double? comprimento, double? valor, int? estoque,
            string ordem, int itensPg, int pgAtual)
        {
            var sql = "SELECT * FROM Produtos WHERE ";
            if (id != null)
            {
                sql += "Id = @id and ";
            }
            if (nome != null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and ";
            }
            if (centroDeDistribuicao != null)
            {
                sql += "CentroDeDistribuicao LIKE \"%" + centroDeDistribuicao + "%\" and ";
            }
            if (status != null)
            {
                sql += "Status = @status and ";
            }
            if (peso != null)
            {
                sql += "Peso = @peso and ";
            }
            if (altura != null)
            {
                sql = "Altura = @altura and ";
            }
            if (largura != null)
            {
                sql += "Largura = @largura and ";
            }
            if (comprimento != null)
            {
                sql += "Comprimento = @comprimento and ";
            }
            if (valor != null)
            {
                sql += "Valor = @valor and ";
            }
            if (estoque != null)
            {
                sql += "Estoque = @estoque and ";
            }
            if (id == null && nome == null && centroDeDistribuicao == null && status == null && peso == null
                && altura == null && largura == null && comprimento == null && valor == null && estoque == null)
            {
                var RetiraWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(RetiraWhere);
            }
            else
            {
                var RetiraAnd = sql.LastIndexOf("and");
                sql = sql.Remove(RetiraAnd);
            }
            if (ordem != null)
            {
                if (ordem == "desc")
                {
                    sql += "ORDER BY Nome DESC";
                }
                else
                {
                    sql += "ORDER BY Nome";
                }
            }
            Console.WriteLine(sql);
            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                var result = await conexao.QueryAsync<ReadProdutoDto>(sql, new
                {
                    Id = id,
                    Nome = nome,
                    CentroDeDistribuicao = centroDeDistribuicao,
                    Staus = status,
                    Peso = peso,
                    Altura = altura,
                    Largura = largura,
                    Comprimento = comprimento,
                    Valor = valor,
                    Estoque = estoque,
                });
                if (pgAtual > 0 && itensPg > 0 && itensPg <= 10)
                {
                    var resultEnd = result.Skip((pgAtual - 1) * itensPg).Take(itensPg).ToList();
                    return resultEnd;
                }
                var resultadoNotPag = result.Skip(0).Take(25).ToList();
                return resultadoNotPag;

            }
        }
        public List<Produto> ListaProdutoPorIdSubcategoria(int id)
        {
            var subcategoria = subcategoriaDao.BuscaSubcategoriaPorId(id);
            try 
            {
                return _context.Produtos.Where(produto => produto.SubcategoriaId == subcategoria.Id).ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<Produto> ListaProdutoPorIdCentro(int id)
        {
            var produto = _context.Produtos.Where(produto => produto.CentroId == id);

            return produto.ToList();
        }
        public Produto BuscaProdutoPorId(int id)
        {
            return _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        }
    }
}
