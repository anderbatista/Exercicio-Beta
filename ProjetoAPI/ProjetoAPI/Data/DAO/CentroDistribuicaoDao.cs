using AutoMapper;
using FluentResults;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProjetoAPI.Data.DTOs.CentroDistribuicaoDto;
using ProjetoAPI.Data.Interfaces;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoAPI.Data.Dtos.ConsultaCepDto;


namespace ProjetoAPI.Data.DAO
{
    public class CentroDistribuicaoDao : ICentroDistribuicaoDao
    {
        private readonly IConfiguration _config;
        private AppDbContext _context;
        private IMapper _mapper;
        private IProdutoDao produtoDao;
        public CentroDistribuicaoDao(IConfiguration configuration, AppDbContext context, IMapper mapper, IProdutoDao pDao)
        {
            _config = configuration;
            _context = context;
            _mapper = mapper;
            produtoDao = pDao;
        }
        public async Task<CreateCentroDistribuicaoDto> AddCentro(CreateCentroDistribuicaoDto createCdDto)
        {
            var sql = "INSERT INTO CentrosD (Nome, Logradouro, Numero, Complemento, Bairro, Localidade, UF, Cep, Status, DataCriacao, DataAlteracao)" +
                "VALUES(@Nome, @Logradouro, @Numero, @Complemento, @Bairro, @Localidade, @UF, @Cep, @Status, @DataCriacao, @DataAlteracao);" +
                "SELECT LAST_INSERT_ID();";

            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                createCdDto.Id = await conexao.QuerySingleAsync<int>(sql, createCdDto);
                return createCdDto;
            }
        }
        public Result UpdateCentro(UpdateCentroDistribuicaoDto updateCentro, int id, ReadConsultaCepDto endereco)
        {
            updateCentro.Logradouro = endereco.Logradouro;
            updateCentro.Bairro = endereco.Bairro;
            updateCentro.Localidade = endereco.Localidade;
            updateCentro.UF = endereco.UF;

            var centroDistribuicao = CentroPorId(id);
            _mapper.Map(updateCentro, centroDistribuicao);
            _context.SaveChanges();
            return Result.Ok();
        }
        public async Task<int> DeletarCentro(ReadCentroDistribuicaoDto centro)
        {
            var sql = "DELETE FROM CentrosD WHERE Id = @Id";
            using (var conexao = new MySqlConnection(_config.GetConnectionString("CategoriaConnection")))
            {
                var result = await conexao.ExecuteAsync(sql, centro);
                return result;
            }
        }
        public List<CentroDistribuicao> CentroPorNome(string nome)
        {
            return _context.CentrosD.Where(c => c.Nome.ToLower().Equals(nome.ToLower())).ToList();
        }
        public List<CentroDistribuicao> CentroPorCep(string cep)
        {
            return _context.CentrosD.Where(c => c.Cep.Equals(cep)).ToList();
        }
        public CentroDistribuicao CentroPorId(int id)
        {
            return _context.CentrosD.FirstOrDefault(cd => cd.Id == id);
        }
        public List<ReadCentroDistribuicaoDto> PesquisaCentroPersonalizada(ReadCentroDistribuicaoDto readCd)
        {
            var sql = "SELECT * FROM CentrosD WHERE ";
            if (readCd.Id != null)
            {
                sql += "Id = @id and ";
            }
            if (readCd.Nome != null)
            {
                sql += "Nome LIKE \"%" + readCd.Nome + "%\" and ";
            }
            if (readCd.Numero != null)
            {
                sql += "Numero = @numero and ";
            }
            if (readCd.Logradouro != null)
            {
                sql += "Logradouro LIKE \"%" + readCd.Logradouro + "%\" and ";
            }
            if (readCd.Complemento != null)
            {
                sql = "Complemento LIKE \"%" + readCd.Complemento + "%\" and ";
            }
            if (readCd.Bairro != null)
            {
                sql += "Bairro LIKE \"%" + readCd.Bairro + "%\" and ";
            }
            if (readCd.Localidade != null)
            {
                sql += "Localidade LIKE \"%" + readCd.Localidade + "%\" and ";
            }
            if (readCd.Uf != null)
            {
                sql += "Uf = @uf and";
            }
            if (readCd.Status != null)
            {
                sql += "Status = @status and";
            }
            if (readCd.DataCriacao != null)
            {
                sql += "DATE_FORMAT(DataCriacao ,'%d/%m/%Y') = @dataCriacao and ";
            }
            if (readCd.DataAlteracao != null)
            {
                sql += "DATE_FORMAT(DataAlteracao ,'%d/%m/%Y') = @dataAlteracao and ";
            }
            if (readCd.Id == null && readCd.Nome == null && readCd.Cep == null && readCd.Logradouro == null
                && readCd.Numero == null && readCd.Complemento == null && readCd.Bairro == null && readCd.Localidade == null
                && readCd.Uf == null && readCd.Status == null && readCd.DataCriacao == null && readCd.DataAlteracao == null)
            {
                var retiraWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(retiraWhere);
            }
            else
            {
                var retiraAnd = sql.LastIndexOf("and");
                sql = sql.Remove(retiraAnd);
            }
            if (readCd.Ordem != null)
            {
                if (readCd.Ordem != null)
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
                var result = conexao.Query<ReadCentroDistribuicaoDto>(sql, new
                {
                    readCd.Id,
                    readCd.Nome,
                    readCd.Cep,
                    readCd.Logradouro,
                    readCd.Numero,
                    readCd.Complemento,
                    readCd.Bairro,
                    readCd.Localidade,
                    readCd.Uf,
                    readCd.Status,
                    readCd.DataCriacao,
                    readCd.DataAlteracao
                });
                if (readCd.PgAtual > 0 && readCd.ItensPg > 0 && readCd.ItensPg <= 10)
                {
                    var resulEnd = result.Skip((readCd.PgAtual - 1) * readCd.ItensPg).Take(readCd.ItensPg).ToList();
                    return resulEnd;
                }
                var resultadoNotPag = result.Skip(0).Take(25).ToList();
                return resultadoNotPag;
            }
        }
        public bool EnderecoUnico(string logradouro, int numero, string complemento)
        {
            string enderecoComparacao = logradouro;
            enderecoComparacao += numero;
            enderecoComparacao += complemento;

            ReadCentroDistribuicaoDto filtro = new ReadCentroDistribuicaoDto();
            List<ReadCentroDistribuicaoDto> listaCD = PesquisaCentroPersonalizada(filtro);

            foreach (var retorno in listaCD)
            {
                string enderecoRetorno = retorno.Logradouro;
                enderecoRetorno += retorno.Numero;
                enderecoRetorno += retorno.Complemento;

                if (enderecoRetorno == enderecoComparacao)
                {
                    return false;
                }
            }
            return true;
        }
        public bool PermiteAlteracaoDoCD(string nome, int id)
        {
            string centroComparacao = nome;
            centroComparacao += id;

            string centroComparacaoNome = nome;

            ReadCentroDistribuicaoDto filtro = new ReadCentroDistribuicaoDto();
            List<ReadCentroDistribuicaoDto> listaCD = PesquisaCentroPersonalizada(filtro);

            //Busca se o nome e igual o ID
            foreach (var retorno in listaCD)
            {
                string centroRetorno = retorno.Nome;
                centroRetorno += retorno.Id;

                if (centroRetorno == centroComparacao)
                {
                    return true;
                }
            }
            //Busca se o nome se consta no banco
            foreach (var retorno in listaCD)
            {
                string centroRetono = retorno.Nome;

                if (centroRetono == centroComparacaoNome)
                {
                    return false;
                }
            }
            return true;
        }
        public void SalvaAlteracao()
        {
            _context.SaveChanges();
        }

    }
}
