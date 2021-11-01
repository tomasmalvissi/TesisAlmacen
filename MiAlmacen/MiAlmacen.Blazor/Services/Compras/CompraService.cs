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
    public class CompraService
    {
        private readonly HttpClient _httpClient;
        public CompraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CompraModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/compras/");
            return JsonConvert.DeserializeObject<IEnumerable<CompraModel>>(await respuesta);
        }

        public async Task<CompraModel> GetUn(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"api/compras/{id}");
            return JsonConvert.DeserializeObject<CompraModel>(await respuesta);
        }

        public async Task<bool> GetNroRecibo(long nroRecibo)
        {
            var respuesta = _httpClient.GetStringAsync($"api/compras/existerecibo/{nroRecibo}");
            return JsonConvert.DeserializeObject<bool>(await respuesta);
        }


        public async Task<HttpResponseMessage> Alta(CompraModel compra)
        {
            string compraSerealizada = JsonConvert.SerializeObject(compra);
            var respuesta = await _httpClient.PostAsync("api/compras/",
                            new StringContent(compraSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/compras/{id}");
            return respuesta;
        }
    }
}