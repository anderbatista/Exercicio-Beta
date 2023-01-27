using AutoMapper;
using FluentResults;
using Newtonsoft.Json;
using ProjetoAPI.Data;
using ProjetoAPI.Data.DTOs.CentroDistribuicaoDto;
using ProjetoAPI.Data.Interfaces;
using ProjetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace ProjetoAPI.Services
{
    public class CentroDistribuicaoService
    {
        private IMapper _mapper;
        private ICentroDistribuicaoDao centroDao;
        private ISubcategoriaDao subcategoriaDao;
        private ICategoriaDao categoriaDao;
        private IProdutoDao produtoDao;

        public CentroDistribuicaoService(ICentroDistribuicaoDao cdao, ICategoriaDao dao, ISubcategoriaDao sdao, IProdutoDao pdao, IMapper mapper)
        {
            _mapper = mapper;
            centroDao = cdao;
            subcategoriaDao = sdao;
            categoriaDao = dao;
            produtoDao = pdao;
        }
        public async Task<CentroDistribuicao> ConsultaEnderecoViaCep(string cep)
        {
            HttpClient client = new HttpClient();
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            var solicitacaoApi = await client.GetAsync(url);
            var retornoApi = await solicitacaoApi.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<CentroDistribuicao>(retornoApi);

            return endereco;
        }
        public async Task<Result> CadastrarCentro(CreateCentroDistribuicaoDto createCdDto)
        {
            if (createCdDto.Status == false)
            {
                return Result.Fail("Impossível cadastrar Centro de Distruibuição com status inativo.");
            }
            var centroNome = centroDao.CentroPorNome(createCdDto.Nome);
            if (centroNome.Count > 0)
            {
                return Result.Fail("Não foi possível cadastrar, este nome de CD já consta em nosso banco de dados.");
            }
            var centroCep = centroDao.CentroPorCep(createCdDto.Cep);
            if (centroCep.Count > 0)
            {
                return Result.Fail("Logradouro já existente, favor verifique se está digitando corretamente.");
            }
            var centro = await ConsultaEnderecoViaCep(createCdDto.Cep);
            var mapeamento = _mapper.Map<CreateCentroDistribuicaoDto>(createCdDto);
            mapeamento.Logradouro = centro.Logradouro;
            mapeamento.Bairro = centro.Bairro;
            mapeamento.Localidade = centro.Localidade;
            mapeamento.UF = centro.UF;
            var cdcadastrado = await centroDao.AddCentro(mapeamento);
            return Result.Ok();
            //return cdcadastrado;
        }
        public async Task<Result> EditarCentro(UpdateCentroDistribuicaoDto centro)
        {
            var cdBusca = centroDao.CentroPorId(centro.Id);
            if (cdBusca == null)
            {
                return Result.Fail("Centro de ditribuição não encontrado");
            }
            if (String.IsNullOrEmpty(centro.Cep))
            {
                centro.Cep = cdBusca.Cep;
            }
            var endereco = await ConsultaEnderecoViaCep(centro.Cep);
            if (endereco == null)
            {
                return Result.Fail("CEP não encontrado");
            }
            if (centroDao.CentroPorCep(centro.Cep).Count > 0)
            {
                return Result.Fail("Logradouro já existente");
            }
            if (centroDao.CentroPorNome(centro.Nome).Count > 0)
            {
                return Result.Fail("Nome já está em uso, digite um nome diferente");
            }
            if (centro.Status == false)
            {
                var produtos = produtoDao.ListaProdutoPorIdCentro(cdBusca.Id);
                if (produtos.Count == 0)
                {
                    if (centro.Cep != cdBusca.Cep)
                    {
                        return centroDao.UpdateCentro(centro, centro.Id, endereco);
                    }
                }
                return Result.Fail("Não foi possível inativar, existe produtos associado");
            }
            return centroDao.UpdateCentro(centro, centro.Id, endereco);
        }
        public async Task<int> DeletarCentro(ReadCentroDistribuicaoDto centro)
        {
            return await centroDao.DeletarCentro(centro);
        }
        public async Task<List<ReadCentroDistribuicaoDto>> PesquisaCentroPersonalizada(ReadCentroDistribuicaoDto readCd)
        {
            return await centroDao.PesquisaCentroPersonalizada(readCd);
        }
    }
}
