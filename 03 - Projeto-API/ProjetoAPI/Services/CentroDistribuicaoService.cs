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
        private ConsultaCepService consultaCepService;

        public CentroDistribuicaoService(ICentroDistribuicaoDao cdao, ICategoriaDao dao, ISubcategoriaDao sdao, IProdutoDao pdao, IMapper mapper, ConsultaCepService CepService)
        {
            _mapper = mapper;
            centroDao = cdao;
            subcategoriaDao = sdao;
            categoriaDao = dao;
            produtoDao = pdao;
            consultaCepService = CepService;
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
                return Result.Fail("Não foi possível cadastrar, este nome de CD já consta no banco de dados.");
            }

            var enderecoUnico = centroDao.EnderecoUnico(createCdDto.Logradouro, createCdDto.Numero, createCdDto.Complemento);
            if (enderecoUnico == false)
            {
                return Result.Fail("Endereço já cadastrado no banco de dados, favor verifique se está digitando corretamente.");
            }

            var centro = await consultaCepService.ConsultaEnderecoViaCep(createCdDto.Cep);
            if (centro.Logradouro == null)
            {
                return Result.Fail("Não foi possível cadastrar endereço, verifique o CEP digitado.");
            }

            var mapeamento = _mapper.Map<CreateCentroDistribuicaoDto>(createCdDto);
            mapeamento.Logradouro = centro.Logradouro;
            mapeamento.Bairro = centro.Bairro;
            mapeamento.Localidade = centro.Localidade;
            mapeamento.UF = centro.UF;
            var cdcadastrado = await centroDao.AddCentro(mapeamento);
            return Result.Ok();
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

            var endereco = await consultaCepService.ConsultaEnderecoViaCep(centro.Cep);
            if (endereco == null)
            {
                return Result.Fail("CEP não encontrado");
            }

            if (centroDao.EnderecoUnico(endereco.Logradouro, centro.Numero, centro.Complemento) == false)
            {
                return Result.Fail("Endereço já cadastrado no banco de dados, favor verifique se está digitando corretamente.");
            }
            
            if (centroDao.PermiteAlteracaoDoCD(centro.Nome, centro.Id))
            {
                return centroDao.UpdateCentro(centro, centro.Id, endereco);
            }
            return Result.Fail("Nome já está em uso, digite um nome diferente");
        }

        public Result EditarStatusCentro(int id)
        {
            var cdBusca = centroDao.CentroPorId(id);            
            if (cdBusca == null)
            {
                return Result.Fail("Centro de ditribuição não encontrado");
            }
            if (cdBusca.Status == true)
            {
                var produtos = produtoDao.ListaProdutoPorIdCentro(cdBusca.Id);
                if (produtos.Count == 0)
                {
                    cdBusca.Status = false;
                    cdBusca.DataAlteracao = DateTime.Now;
                    centroDao.SalvaAlteracao();
                    return Result.Ok();
                }
                return Result.Fail("Não foi possível inativar, existe produtos associado");
            }
            cdBusca.Status = true;
            cdBusca.DataAlteracao = DateTime.Now;
            centroDao.SalvaAlteracao();
            return Result.Ok();
        }

        public async Task<int> DeletarCentro(ReadCentroDistribuicaoDto centro)
        {
            return await centroDao.DeletarCentro(centro);
        }
        public List<ReadCentroDistribuicaoDto> PesquisaCentroPersonalizada(ReadCentroDistribuicaoDto readCd)
        {
            return centroDao.PesquisaCentroPersonalizada(readCd);
        }
    }
}
