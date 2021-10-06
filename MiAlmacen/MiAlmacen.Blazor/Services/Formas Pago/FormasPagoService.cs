using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class FormasPagoService
    {
        private readonly HttpClient _httpClient;
        public FormasPagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FormaPagoModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync("api/formaspago");
            return JsonConvert.DeserializeObject<IEnumerable<FormaPagoModel>>(await respuesta);
        }
    }
}