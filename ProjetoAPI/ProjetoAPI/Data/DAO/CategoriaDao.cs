using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoAPI.Data.Dtos.CategoriaDto;

namespace ProjetoAPI.Data.Repository
{
    public class CategoriaDao : ICategoriaDao
    {
        private readonly IConfiguration _config;
        private AppDbContext _context;
        private IMapper _mapper;
        public CategoriaDao(IConfiguration configuration, AppDbContext context, IMapper mapper )
        {
            _config = configuration;
            _context = context;
            _mapper = mapper;
        }

        public void CadastrarCategoria(CreateCategoriaDto CategoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(CategoriaDto);
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public IQueryable<Categoria> ListarCategorias()
        {
            return _context.Categorias;
        }

        public IQueryable<Categoria> ListarCategoriasPorNome(string nome)
        {
            return _context.Categorias.Where(c => c.Nome.ToLower().Contains(nome.ToLower()));
        }

        public Categoria BuscarCategoriasPorID(int id)
        {
            return _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        }

        public List<ReadCategoriaDto> PesquisaCategoriaPersonalizada(ReadCategoriaDto readCategoria,
            string ordem, int itensPg, int pgAtual)
        {
            var sql = "SELECT * FROM Categorias WHERE ";
            if (readCategoria.Id != null)
            {
                sql += "Id = @id and ";
            }
            if (readCategoria.Nome != null)
            {
                sql += "Nome LIKE \"%" + readCategoria.Nome + "%\" and ";
            }
            if (readCategoria.Status != null)
            {
                sql += "Status = @status and";
            }
            if (readCategoria.DataCriacao != null)
            {
                sql += "DATE_FORMAT(DataCriacao ,'%d/%m/%Y') = @dataCriacao and ";
            }
            if (readCategoria.DataAlteracao != null)
            {
                sql += "DATE_FORMAT(DataAlteracao ,'%d/%m/%Y') = @dataAlteracao and ";
            }
            if (readCategoria.Id == null && readCategoria.Nome == null && 
                readCategoria.Status == null && readCategoria.DataCriacao == null && readCategoria.DataAlteracao == null)
            {
                var retiraWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(retiraWhere);
            }
            else
            {
                var retiraAnd = sql.LastIndexOf("and");
                sql = sql.Remove(retiraAnd);
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
                var result = conexao.Query<ReadCategoriaDto>(sql, new
                {
                    readCategoria.Id,
                    readCategoria.Nome,
                    readCategoria.Status,
                    readCategoria.DataCriacao,
                    readCategoria.DataAlteracao
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

        public void FinalEditarCategoria(UpdateCategoriaDto novoNomeDto, int id)
        {
            var categoria = BuscarCategoriasPorID(id);
            _mapper.Map(novoNomeDto, categoria);
            _context.SaveChanges();
        }

        public void FinalDeletarCategoria(int id)
        {
            var categoria = BuscarCategoriasPorID(id);
            _context.Remove(categoria);
            _context.SaveChanges();
        }
    }
}
