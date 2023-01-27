using ProjetoAPI.Data.DTOs.CentroDistribuicaoDto;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using FluentResults;

namespace ProjetoAPI.Data.Interfaces
{
    public interface ICentroDistribuicaoDao
    {
        public Task<CreateCentroDistribuicaoDto> AddCentro(CreateCentroDistribuicaoDto createCdDto);
        public Result UpdateCentro(UpdateCentroDistribuicaoDto updateCentro, int id, CentroDistribuicao endereco);
        public Task<int> DeletarCentro(ReadCentroDistribuicaoDto centro);
        public List<CentroDistribuicao> CentroPorNome(string nome);
        public List<CentroDistribuicao> CentroPorCep(string cep);
        public CentroDistribuicao CentroPorId(int id);
        public Task<List<ReadCentroDistribuicaoDto>> PesquisaCentroPersonalizada(ReadCentroDistribuicaoDto readCd);
    }
}
