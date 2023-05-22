using ProjetoAPI.Data.Dtos;
using ProjetoAPI.Data.Dtos.ProdutoDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Data
{
    public interface IProdutoDao
    {
        public Task<CreateProdutoDto> AddProduto(CreateProdutoDto produto);
        public Task<int> EditarProduto(UpdateProdutoDto produto);
        public Task<int> DeletarProduto(ReadProdutoDto produto);
        public Task<List<ReadProdutoDto>> PesquisaPersonalizada(int? id, string nome, string centroDeDistribuicao, bool? status,
            double? peso, double? altura, double? largura,
            double? comprimento, double? valor, int? estoque,
            string ordem, int itensPg, int pgAtual);
        public List<Produto> ListaProdutoPorIdSubcategoria(int id);
        public Produto BuscaProdutoPorId(int id);
        public List<Produto> ListaProdutoPorIdCentro(int id);
    }
}
