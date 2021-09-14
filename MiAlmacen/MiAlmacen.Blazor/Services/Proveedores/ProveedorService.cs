using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<ProveedorModel> Alta(ProveedorModel proveedor)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/proveedores/", proveedor);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProveedorModel>(await obj);
        }

        public async Task<ProveedorModel> Editar(ProveedorModel proveedor)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/proveedores/{proveedor.Id}", proveedor);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProveedorModel>(await obj);
        }

        public async Task<int> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/proveedores/{id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                id = 0;
            }
            return id;
        }
    }
}
