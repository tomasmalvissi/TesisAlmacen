using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;
        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ClienteModel>> GetAllClientes()
        {
            var respuesta = _httpClient.GetStringAsync($"api/clientes/");
            return JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(await respuesta);
        }

        public async Task<ClienteModel> GetUnCliente(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClienteModel>($"api/clientes/{id}");
        }

        public async Task<bool> ExisteCliente(long dni)
        {
            var respuesta = _httpClient.GetStringAsync($"api/clientes/existe/{dni}");
            return JsonConvert.DeserializeObject<bool>(await respuesta);
        }

        public async Task<decimal> GetDeuda(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"api/clientes/deuda/{id}");
            return JsonConvert.DeserializeObject<decimal>(await respuesta);
        }

        public async Task<HttpResponseMessage> Alta(ClienteModel cliente)
        {
            string clienteSerealizado = JsonConvert.SerializeObject(cliente);
            var respuesta = await _httpClient.PostAsync("api/clientes/", 
                        new StringContent(clienteSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Editar(ClienteModel cliente)
        {
            string clienteSerealizado = JsonConvert.SerializeObject(cliente);
            var respuesta = await _httpClient.PutAsync($"api/clientes/{cliente.Id}",
                        new StringContent(clienteSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/clientes/{id}");
            return respuesta;
        }
    }
}
