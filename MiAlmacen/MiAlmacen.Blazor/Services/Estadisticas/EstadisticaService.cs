using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class EstadisticaService
    {
        private readonly HttpClient _httpClient;
        public EstadisticaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PeriodoModel>> GetVentasXPeriodo()
        {
            var respuesta = _httpClient.GetStringAsync($"api/estadisticas/ventas");
            return JsonConvert.DeserializeObject<IEnumerable<PeriodoModel>>(await respuesta);
        }
        public async Task<IEnumerable<PeriodoModel>> GetComprasXPeriodo()
        {
            var respuesta = _httpClient.GetStringAsync($"api/estadisticas/compras");
            return JsonConvert.DeserializeObject<IEnumerable<PeriodoModel>>(await respuesta);
        }

        public async Task<IEnumerable<TopModel>> GetTopProductos()
        {
            var respuesta = _httpClient.GetStringAsync($"api/estadisticas/productos");
            return JsonConvert.DeserializeObject<IEnumerable<TopModel>>(await respuesta);
        }

        public async Task<IEnumerable<TopModel>> GetTopClientes()
        {
            var respuesta = _httpClient.GetStringAsync($"api/estadisticas/clientes");
            return JsonConvert.DeserializeObject<IEnumerable<TopModel>>(await respuesta);
        }
    }
}