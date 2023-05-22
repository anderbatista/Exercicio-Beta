using ProjetoAPI.Data.DTOs.CentroDistribuicaoDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using FluentResults;
using ProjetoAPI.Data.Dtos.ConsultaCepDto;

namespace ProjetoAPI.Data.Interfaces
{
    public interface ICentroDistribuicaoDao
    {
        public Task<CreateCentroDistribuicaoDto> AddCentro(CreateCentroDistribuicaoDto createCdDto);
        public Result UpdateCentro(UpdateCentroDistribuicaoDto updateCentro, int id, ReadConsultaCepDto endereco);
        public Task<int> DeletarCentro(ReadCentroDistribuicaoDto centro);
        public List<CentroDistribuicao> CentroPorNome(string nome);
        public List<CentroDistribuicao> CentroPorCep(string cep);
        public CentroDistribuicao CentroPorId(int id);
        public List<ReadCentroDistribuicaoDto> PesquisaCentroPersonalizada(ReadCentroDistribuicaoDto readCd);
        public bool EnderecoUnico(string logradouro, int numero, string complemento);
        public bool PermiteAlteracaoDoCD(string nome, int id);
        public void SalvaAlteracao();
    }
}
