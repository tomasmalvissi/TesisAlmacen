using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;   
        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;  
        }
        public async Task<IEnumerable<ClienteModel>> GetAllClientes()
        {
            return JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(await _httpClient.GetStringAsync($"api/clientes"));
        }

        //TODO: no va a poder buscar sincronicamente jaja por ende definir que campo buscará
    }
}
