using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;

namespace ProjetoAPI.Data.Repository
{
    public class SubcategoriaDao : ISubcategoriaDao
    {
        private readonly IConfiguration _config;
        private AppDbContext _context;
        private IMapper _mapper;
        public SubcategoriaDao(IConfiguration configuration, AppDbContext context, IMapper mapper)
        {
            _config = configuration;
            _context = context;
            _mapper = mapper;
        }

        public Subcategoria CadastrarSubcategoria(Subcategoria subcategoriaDto)
        {
            _context.Subcategorias.Add(subcategoriaDto);
            _context.SaveChanges();
            return subcategoriaDto;
        }

        public List<Subcategoria> ListaSubcategoriaPorIdcategoria(int id)
        {
            return _context.Subcategorias.Where(subcategoria => subcategoria.CategoriaId == id).ToList();
        }

        public IQueryable<Subcategoria> ListarSubcategoriasPorNome(string nome)
        {
            return _context.Subcategorias.Where(c => c.Nome.ToLower().Contains(nome.ToLower()));
        }

        public IQueryable<Subcategoria> ListarSubcategorias()
        {
            return _context.Subcategorias;
        }

        public Subcategoria BuscaSubcategoriaPorId(int id)
        {
            return _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
        }

        public Categoria BuscaCategoriaPorId(Subcategoria subcategoria)
        {
            return _context.Categorias.FirstOrDefault(c => c.Id == subcategoria.CategoriaId);
        }

        public List<ReadSubcategoriaDto> PesquisaSubcategoriaPersonalizada(ReadSubcategoriaDto readSubcategoria,
           string ordem, int itensPg, int pgAtual)
        {
            var sql = "SELECT * FROM Subcategorias WHERE ";
            if (readSubcategoria.Id != null)
            {
                sql += "Id = @id and ";
            }
            if (readSubcategoria.Nome != null)
            {
                sql += "Nome LIKE \"%" + readSubcategoria.Nome + "%\" and ";
            }
            if (readSubcategoria.Status != null)
            {
                sql += "Status = @status and";
            }
            if (readSubcategoria.DataCriacao != null)
            {
                sql += "DATE_FORMAT(DataCriacao ,'%d/%m/%Y') = @dataCriacao and ";
            }
            if (readSubcategoria.DataAlteracao != null)
            {
                sql += "DATE_FORMAT(DataAlteracao ,'%d/%m/%Y') = @dataAlteracao and ";
            }
            if (readSubcategoria.Id == null && readSubcategoria.Nome == null &&
                readSubcategoria.Status == null && readSubcategoria.DataCriacao == null && readSubcategoria.DataAlteracao == null)
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
                var result = conexao.Query<ReadSubcategoriaDto>(sql, new
                {
                    readSubcategoria.Id,
                    readSubcategoria.Nome,
                    readSubcategoria.Status,
                    readSubcategoria.DataCriacao,
                    readSubcategoria.DataAlteracao
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

        public void FinalEditarSubcategoria(int id, UpdateSubcategoriaDto novoNomeDto)
        {
            var subcategoria = BuscaSubcategoriaPorId(id);
            _mapper.Map(novoNomeDto, subcategoria);
            _context.SaveChanges();
        }

        public void FinalDeletarSubcategoria(int id)
        {
            var subcategoria = BuscaSubcategoriaPorId(id);
            _context.Remove(subcategoria);
            _context.SaveChanges();
        }
    }
}
