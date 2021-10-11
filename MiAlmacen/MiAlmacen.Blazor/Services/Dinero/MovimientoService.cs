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
        public async Task<HttpResponseMessage> Alta(MovimientosCajaModel mov)
        {
            string movSerealizada = JsonConvert.SerializeObject(mov);
            var respuesta = await _httpClient.PostAsync("api/movimientos/",
                            new StringContent(movSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }
    }
}
