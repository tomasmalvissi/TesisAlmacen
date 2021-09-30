using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class CajaService
    {
        private readonly HttpClient _httpClient;
        public CajaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CajaModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/caja/");
            return JsonConvert.DeserializeObject<IEnumerable<CajaModel>>(await respuesta);
        }

        public async Task<CajaModel> GetUn(int id)
        {
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            return await _httpClient.GetFromJsonAsync<CajaModel>($"api/caja/{id}", options);
        }

        public async Task<CajaModel> GetUltimo()
        {
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            return await _httpClient.GetFromJsonAsync<CajaModel>($"api/caja/", options);
        }

        public async Task<CajaModel> Alta(CajaModel caja)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/caja/", caja);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CajaModel>(await obj);
        }

        public async Task<CajaModel> Editar(CajaModel caja)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/caja/{caja.Id}", caja);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CajaModel>(await obj);
        }
    }
}
