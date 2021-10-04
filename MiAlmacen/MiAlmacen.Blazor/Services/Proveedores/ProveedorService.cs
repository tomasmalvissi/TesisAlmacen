using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class ProveedorService
    {
        private readonly HttpClient _httpClient;
        public ProveedorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProveedorModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/proveedores/");
            return JsonConvert.DeserializeObject<IEnumerable<ProveedorModel>>(await respuesta);
        }

        public async Task<ProveedorModel> GetUn(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProveedorModel>($"api/proveedores/{id}");
        }

        public async Task<HttpResponseMessage> Alta(ProveedorModel proveedor)
        {
            string provSerealizado = JsonConvert.SerializeObject(proveedor);
            var respuesta = await _httpClient.PostAsync("api/proveedores/",
                      new StringContent(provSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Editar(ProveedorModel proveedor)
        {
            string provSerealizado = JsonConvert.SerializeObject(proveedor);
            var respuesta = await _httpClient.PutAsync($"api/proveedores/{proveedor.Id}",
                      new StringContent(provSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/proveedores/{id}");
            return respuesta;
        }
    }
}
