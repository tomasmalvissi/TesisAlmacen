using MiAlmacen.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<Clientes>> GetAllClientes()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Clientes>>(await _httpClient.GetStringAsync($"api/clientes"));
        }
    }
}
