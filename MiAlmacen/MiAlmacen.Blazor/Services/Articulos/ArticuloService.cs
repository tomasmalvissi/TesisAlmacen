using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class ArticuloService
    {
        private readonly HttpClient _httpClient;
        public ArticuloService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ArticuloModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/articulos/");
            return JsonConvert.DeserializeObject<IEnumerable<ArticuloModel>>(await respuesta);
        }

        public async Task<ArticuloModel> GetUn(int id)
        {
            return await _httpClient.GetFromJsonAsync<ArticuloModel>($"api/articulos/{id}");
        }

        public async Task<HttpResponseMessage> Alta(ArticuloModel articulo)
        {
            string articuloSerealizada = JsonConvert.SerializeObject(articulo);
            var respuesta = await _httpClient.PostAsync("api/articulos/",
                            new StringContent(articuloSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Editar(ArticuloModel articulo)
        {
            string articuloSerealizada = JsonConvert.SerializeObject(articulo);
            var respuesta = await _httpClient.PutAsync($"api/articulos/{articulo.Id}", 
                            new StringContent(articuloSerealizada, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/articulos/{id}");
            return respuesta;
        }
    }
}
