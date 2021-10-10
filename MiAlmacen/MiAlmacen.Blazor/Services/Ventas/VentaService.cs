using MiAlmacen.Data.Entities;
using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
        public async Task<IEnumerable<VentaModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/ventas/");
            return JsonConvert.DeserializeObject<IEnumerable<VentaModel>>(await respuesta);
        }

        public async Task<VentaModel> GetUn(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"api/ventas/{id}");
            return JsonConvert.DeserializeObject<VentaModel>(await respuesta);
        }

        public async Task<HttpResponseMessage> Alta(VentaModel venta)
        {
            string ventaSerealizada = JsonConvert.SerializeObject(venta);
            var respuesta = await _httpClient.PostAsync("api/ventas/",
                            new StringContent(ventaSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> EditarSaldo(VentaModel venta)
        {
            string ventaSerealizada = JsonConvert.SerializeObject(venta);
            var respuesta = await _httpClient.PutAsync($"api/ventas/", 
                            new StringContent(ventaSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/ventas/{id}");
            return respuesta;
        }
    }
}
