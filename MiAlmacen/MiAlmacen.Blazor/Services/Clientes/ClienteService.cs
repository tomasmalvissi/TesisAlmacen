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

        public async Task<ClienteModel> GetUnCliente(int id)
        {
            return JsonConvert.DeserializeObject<ClienteModel>(await _httpClient.GetStringAsync($"api/clientes/{id}"));
        }

        public async Task<ClienteModel> Alta(ClienteModel cliente)
        {
            var respuesta = await _httpClient.PostAsync("api/clientes/crear", cliente);
            return respuesta;
        }

        public async Task<ClienteModel> Editar(ClienteModel cliente)
        {
            var respuesta = await _httpClient.PutAsync("api/clientes", cliente);
            return respuesta;
        }

        public async Task<int> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/clientes/{id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                id = 0;
            }
            return id;
        }
    }
}
