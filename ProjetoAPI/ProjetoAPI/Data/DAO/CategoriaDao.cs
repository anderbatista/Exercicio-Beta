using AutoMapper;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Models;
using System.Linq;

namespace ProjetoAPI.Data.Repository
{
    public class CategoriaDao : ICategoriaDao
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public CategoriaDao(AppDbContext context, IMapper mapper)
        {
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
