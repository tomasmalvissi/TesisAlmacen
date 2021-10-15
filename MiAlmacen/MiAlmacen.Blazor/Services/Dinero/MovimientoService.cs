using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class MovimientoService
    {
        private readonly HttpClient _httpClient;
        public MovimientoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MovimientosCajaModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/movimientos/");
            return JsonConvert.DeserializeObject<IEnumerable<MovimientosCajaModel>>(await respuesta);
        }
    }
}
