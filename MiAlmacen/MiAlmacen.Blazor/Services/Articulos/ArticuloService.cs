using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services.Articulos
{
    public class ArticuloService
    {
        private readonly HttpClient _httpClient;
        public ArticuloService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ArticuloModel>> GetAllClientes()
        {
            var respuesta = _httpClient.GetStringAsync($"api/articulos/");
            return JsonConvert.DeserializeObject<IEnumerable<ArticuloModel>>(await respuesta);
        }

        public async Task<ArticuloModel> GetUnCliente(int id)
        {
            return await _httpClient.GetFromJsonAsync<ArticuloModel>($"api/articulos/{id}");
        }

        public async Task<ArticuloModel> Alta(ArticuloModel articulo)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/articulos/", articulo);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ArticuloModel>(await obj);
        }

        public async Task<ArticuloModel> Editar(ArticuloModel articulo)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/articulos/{articulo.Id}", articulo);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ArticuloModel>(await obj);
        }

        public async Task<int> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/articulos/{id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                id = 0;
            }
            return id;
        }
    }
}
