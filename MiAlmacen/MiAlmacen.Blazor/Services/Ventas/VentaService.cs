using MiAlmacen.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class VentaService
    {
        private readonly HttpClient _httpClient;
        public VentaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Ventas>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/ventas/");
            return JsonConvert.DeserializeObject<IEnumerable<Ventas>>(await respuesta);
        }

        public async Task<Ventas> GetUn(int id)
        {
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            return await _httpClient.GetFromJsonAsync<Ventas>($"api/ventas/{id}", options);
        }

        public async Task<Ventas> Alta(Ventas venta)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/ventas/", venta);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Ventas>(await obj);
        }

        public async Task<Ventas> EditarSaldo(Ventas venta, float nuevoSaldo)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/ventas/{nuevoSaldo}", venta);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Ventas>(await obj);
        }

        public async Task<int> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/ventas/{id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                id = 0;
            }
            return id;
        }
    }
}
