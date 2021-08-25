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

        public async Task<int> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/clientes/{id}");
            if (respuesta.IsSuccessStatusCode)
            {
                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
