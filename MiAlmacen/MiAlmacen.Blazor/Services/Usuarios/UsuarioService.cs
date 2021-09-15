using MiAlmacen.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UsuarioModel>> GetAll()
        {
            var respuesta = _httpClient.GetStringAsync($"api/usuarios/");
            return JsonConvert.DeserializeObject<IEnumerable<UsuarioModel>>(await respuesta);
        }

        public async Task<UsuarioModel> GetUn(int id)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioModel>($"api/usuarios/{id}");
        }

        public async Task<UsuarioModel> Alta(UsuarioModel usuario)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/usuarios/", usuario);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UsuarioModel>(await obj);
        }

        public async Task<UsuarioModel> Editar(UsuarioModel usuario)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/usuarios/{usuario.Id}", usuario);
            var obj = respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UsuarioModel>(await obj);
        }

        public async Task<int> Eliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                id = 0;
            }
            return id;
        }
    }
}
