using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EnderecoService
    {
        public async Task<Usuario> ConsultaEnderecoViaCep(string cep)
        {
            HttpClient client = new HttpClient();
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            var solicitacaoApi = await client.GetAsync(url);
            var retornoApi = await solicitacaoApi.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<Usuario>(retornoApi);

            return endereco;
        }
    }
}
