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
    public class SalidaDineroService
    {
        private readonly HttpClient _httpClient;
        public SalidaDineroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SalidasDineroModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/salida-dinero/");
            return JsonConvert.DeserializeObject<IEnumerable<SalidasDineroModel>>(await respuesta);
        }

        public async Task<HttpResponseMessage> Alta(SalidasDineroModel sd)
        {
            string sdSerealizada = JsonConvert.SerializeObject(sd);
            var respuesta = await _httpClient.PostAsync("api/salida-dinero/",
                            new StringContent(sdSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }
    }
}
