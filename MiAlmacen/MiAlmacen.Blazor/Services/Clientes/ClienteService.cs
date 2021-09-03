using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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
        public async Task<IEnumerable<ClienteModel>> GetAllClientes(string filtro)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(await _httpClient.GetStringAsync($"api/clientes/{filtro}"));
        }

        public async Task<ClienteModel> GetUnCliente(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClienteModel>($"api/clientes/{id}");
        }

        public async Task<ClienteModel> Alta(ClienteModel cliente)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/clientes/", cliente);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ClienteModel>(await obj);
        }

        public async Task<ClienteModel> Editar(ClienteModel cliente)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/clientes/{cliente.Id}", cliente);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ClienteModel>(await obj);
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
