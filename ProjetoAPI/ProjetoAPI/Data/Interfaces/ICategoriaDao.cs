using ProjetoAPI.Data.Dtos;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoAPI.Data
{
    public interface ICategoriaDao
    {
        IQueryable<Categoria> ListarCategorias();
        IQueryable<Categoria> ListarCategoriasPorNome(string nome);
        void CadastrarCategoria(CreateCategoriaDto CategoriaDto);
        Categoria BuscarCategoriasPorID(int id);
        void FinalEditarCategoria(UpdateCategoriaDto novoNomeDto, int id);
        void FinalDeletarCategoria(int id);
        public List<ReadCategoriaDto> PesquisaCategoriaPersonalizada(ReadCategoriaDto readCategoria,
            string ordem, int itensPg, int pgAtual);
    }
}
