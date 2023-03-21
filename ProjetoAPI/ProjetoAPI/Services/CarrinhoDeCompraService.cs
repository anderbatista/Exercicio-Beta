using AutoMapper;
using ProjetoAPI.Data;
using ProjetoAPI.Data.Dtos.CarrinhoDeCompra;
using ProjetoAPI.Data.Dtos.CarrinhoProdutoDto;
using ProjetoAPI.Data.Interfaces;
using ProjetoAPI.Models;
using System.Threading.Tasks;
using ProjetoAPI.Middleware.Exceptions;
using System.Collections.Generic;

namespace ProjetoAPI.Services
{
    public class CarrinhoDeCompraService
    {
        private IMapper _mapper;
        private IProdutoDao produtoDao;
        private ICarrinhoDeCompraDao carrinhoDeCompraDao;
        private IProdutoNoCarrinhoDao produtoNoCarrinhoDao;
        private ConsultaCepService consultaCepService;

        public CarrinhoDeCompraService(IMapper mapper, IProdutoDao pdao, ICarrinhoDeCompraDao carrinhoDeCompraDao,
            IProdutoNoCarrinhoDao cpDao, ConsultaCepService consultaCepService)
        {
            _mapper = mapper;
            produtoDao = pdao;
            produtoNoCarrinhoDao = cpDao;
            this.carrinhoDeCompraDao = carrinhoDeCompraDao;
            this.consultaCepService = consultaCepService;
        }

        public async Task<CarrinhoDeCompra> CriarCarrinho(CreateCarrinhoDeCompraDto createCarrinhoDto)
        {
            var carrinho = new CarrinhoDeCompra();

            var buscaCep = await consultaCepService.ConsultaEnderecoViaCep(createCarrinhoDto.Cep);
            if (buscaCep.Logradouro == null)
            {
                throw new NullEx("Endereço não localizado, verifique o CEP digitado. ");
            }

            var mapeamento = _mapper.Map<CarrinhoDeCompra>(createCarrinhoDto);
            mapeamento.Logradouro = buscaCep.Logradouro;
            mapeamento.Bairro = buscaCep.Bairro;
            mapeamento.Localidade = buscaCep.Localidade;
            mapeamento.Uf = buscaCep.UF;

            var carrinhoCadastrado = carrinhoDeCompraDao.CriaCarrinhoDeCompra(mapeamento);
            return carrinhoCadastrado;
        }

        public ProdutoNoCarrinho AdicionarProduto(CreateProdutoNoCarrinhoDto carrinhoProdutoDto)
        {
            var buscaProduto = produtoDao.BuscaProdutoPorId(carrinhoProdutoDto.IdProduto);
            if (buscaProduto == null)
            {
                throw new NullEx("Produto não localizado. ");
            }

            var carrinho = carrinhoDeCompraDao.BuscaCarrinhoPorId(carrinhoProdutoDto.IdCarrinho);
            if (carrinho == null)
            {
                throw new NullEx("Carrinho não localizado.");
            }

            var relacaoCriada = _mapper.Map<CreateProdutoNoCarrinhoDto>(carrinhoProdutoDto);
            relacaoCriada.NomeProduto = buscaProduto.Nome;
            relacaoCriada.ValorUnitarioProduto = buscaProduto.Valor;

            var salvaRelacao = _mapper.Map<ProdutoNoCarrinho>(relacaoCriada);

            var buscaSeProdutoJaEstaNoCarrinho = produtoNoCarrinhoDao.BuscaItemNoCarrinho(carrinhoProdutoDto);

            if (buscaSeProdutoJaEstaNoCarrinho == null)
            {
                if (buscaProduto.QtdeEstoque > 0 && buscaProduto.Status == true)
                {
                    for (int item = 0; item < carrinhoProdutoDto.QuantidadeProduto; item++)
                    {
                        carrinho.ProdutosNoCarrinho.Add(salvaRelacao);
                        produtoNoCarrinhoDao.SalvaProdutoNoCarrinho(relacaoCriada);
                    }
                    SalvaCarrinhoDeCompra(carrinho);
                }

                if (buscaProduto.QtdeEstoque == 0 || buscaProduto.Status != true)
                {
                    throw new NullEx("Produto inativo ou sem estoque. ");
                }
                return salvaRelacao;
            }

            buscaSeProdutoJaEstaNoCarrinho.QuantidadeProduto += carrinhoProdutoDto.QuantidadeProduto;
            produtoNoCarrinhoDao.SalvaProdutoNoCarrinho(relacaoCriada);

            SalvaCarrinhoDeCompra(carrinho);

            return buscaSeProdutoJaEstaNoCarrinho;
        }

        public ProdutoNoCarrinho RemoverProduto(CreateProdutoNoCarrinhoDto carrinhoProdutoDto)
        {
            var carrinho = carrinhoDeCompraDao.BuscaCarrinhoPorId(carrinhoProdutoDto.IdCarrinho);
            if (carrinho == null)
            {
                throw new NullEx("Carrinho não localizado.");
            }

            var buscaProdutoNoCarrinho = produtoNoCarrinhoDao.BuscaItemNoCarrinho(carrinhoProdutoDto);
            var validaRemocaoProduto = buscaProdutoNoCarrinho.QuantidadeProduto -= carrinhoProdutoDto.QuantidadeProduto;

            if (validaRemocaoProduto <= 0)
            {
                produtoNoCarrinhoDao.DeletaProdutoNoCarrinho(buscaProdutoNoCarrinho.Id);
                produtoNoCarrinhoDao.SalvaProdutoNoCarrinho(carrinhoProdutoDto);

                SalvaCarrinhoDeCompra(carrinho); 
            }
            else
            {
                carrinho.QuantidadeTotalDeProdutos = validaRemocaoProduto;
                var mapeamento = _mapper.Map<CreateProdutoNoCarrinhoDto>(buscaProdutoNoCarrinho);

                produtoNoCarrinhoDao.SalvaProdutoNoCarrinho(mapeamento);
                SalvaCarrinhoDeCompra(carrinho);
            }

            return buscaProdutoNoCarrinho;
        }

        private void SalvaCarrinhoDeCompra(CarrinhoDeCompra carrinho)
        {
            var buscaTotais = carrinhoDeCompraDao.AtualizaTotaisDoCarrinho(carrinho);
            if (buscaTotais == null)
            {
                carrinho.QuantidadeTotalDeProdutos = 0;
                carrinho.ValorTotalCarrinho = 0;
            }
            else
            {
                carrinho.QuantidadeTotalDeProdutos = buscaTotais.QuantidadeTotalDeProdutos;
                carrinho.ValorTotalCarrinho = buscaTotais.ValorTotalCarrinho;
            }

            carrinhoDeCompraDao.SalvaAlteracaoCarrinhoDeCompra();
        }

        public IReadOnlyList<CarrinhoDeCompra> ListarCarrinho()
        {
            return carrinhoDeCompraDao.ListaCarrinho();
        }

        public CarrinhoDeCompra ListarCarrinhoPorId(int? id)
        {
            return carrinhoDeCompraDao.BuscaCarrinhoPorId(id);
        }

    }
}
