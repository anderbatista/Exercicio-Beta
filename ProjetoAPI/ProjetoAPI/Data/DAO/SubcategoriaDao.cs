using AutoMapper;
using ProjetoAPI.Data.Dtos;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoAPI.Data.Repository
{
    public class SubcategoriaDao : ISubcategoriaDao
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public SubcategoriaDao(AppDbContext context, IMapper mapper)
        {
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
