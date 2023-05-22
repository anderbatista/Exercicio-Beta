using Newtonsoft.Json;
using ProjetoAPI.Data.Dtos.ConsultaCepDto;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjetoAPI.Services
{
    public class ConsultaCepService
    {
        public async Task<ReadConsultaCepDto> ConsultaEnderecoViaCep(string cep)
        {
            HttpClient client = new HttpClient();
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            var solicitacaoApi = await client.GetAsync(url);
            var retornoApi = await solicitacaoApi.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<ReadConsultaCepDto>(retornoApi);
           
            return endereco;
        }
    }
}