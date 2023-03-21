using AutoMapper;
using ProjetoAPI.Data.Dtos.CarrinhoProdutoDto;
using ProjetoAPI.Data.Interfaces;
using ProjetoAPI.Models;
using System.Linq;

namespace ProjetoAPI.Data.DAO
{
    public class ProdutoNoCarrinhoDao : IProdutoNoCarrinhoDao
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ProdutoNoCarrinhoDao(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ProdutoNoCarrinho BuscaItemNoCarrinho(CreateProdutoNoCarrinhoDto buscaProdutoNoCarrinho)
        {
            var buscaSeExistir = _context.ProdutosNoCarrinho.SingleOrDefault(
                c => c.IdCarrinho == buscaProdutoNoCarrinho.IdCarrinho
                && c.IdProduto == buscaProdutoNoCarrinho.IdProduto);

            return buscaSeExistir;
        }

        public ProdutoNoCarrinho BuscaProdutoNoCarrinhoPorId(int id)
        {
            return _context.ProdutosNoCarrinho.FirstOrDefault(p => p.Id == id);
        }

        public CreateProdutoNoCarrinhoDto SalvaProdutoNoCarrinho(CreateProdutoNoCarrinhoDto criaRelacao)
        {
            _context.SaveChanges();
            return criaRelacao;
        }

        public void DeletaProdutoNoCarrinho(int id)
        {
            var produtoNoCarrinho = BuscaProdutoNoCarrinhoPorId(id);
            _context.Remove(produtoNoCarrinho);
        }

    }
}
