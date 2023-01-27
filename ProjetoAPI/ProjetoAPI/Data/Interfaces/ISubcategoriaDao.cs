using ProjetoAPI.Data.Dtos;
using ProjetoAPI.Data.Dtos.SubcategoriaDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoAPI.Data
{
    public interface ISubcategoriaDao
    {
        List<Subcategoria> ListaSubcategoriaPorIdcategoria(int id);
        Subcategoria CadastrarSubcategoria(Subcategoria SubcategoriaDto);
        IQueryable<Subcategoria> ListarSubcategoriasPorNome(string nome);
        IQueryable<Subcategoria> ListarSubcategorias();
        Categoria BuscaCategoriaPorId(Subcategoria subcategoria);
        Subcategoria BuscaSubcategoriaPorId(int id);
        void FinalEditarSubcategoria(int id, UpdateSubcategoriaDto novoNomeDto);
        void FinalDeletarSubcategoria(int id);

    }
}
