using MiAlmacen.Blazor.Services.Usuarios;
using MiAlmacen.Model.Models;
using Microsoft.JSInterop;
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
        private IJSRuntime _jsRuntime;
        public UsuarioService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
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


        //login service


        public async Task<int> Login(string username, string pass)
        {
            var respuesta = await _httpClient.GetAsync($"api/usuarios/{username}/{pass}");
            int id = 0;
            if (respuesta.IsSuccessStatusCode)
            {
                var obj = respuesta.Content.ReadAsStringAsync();
                id = JsonConvert.DeserializeObject<int>(await obj);
                string token = TokenGenerator();
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "Token", token);
            }
            return id;
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "Token");
        }

        public async Task<string> GetSesion()
        {
            string result = String.Empty;
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "Token");
            if (token == null)
            {
                result = null;
            }
            else
            {
                result = JsonConvert.DeserializeObject<string>(token);
            }
            return result;
        }
        public async Task<UsuarioModel> SetUsuarioLog(int id)
        {
            var usuarioregistrado = await GetUn(id);
            return usuarioregistrado;
        }
        private string TokenGenerator()
        {
            Random r = new Random();
            string numero = "";
            for (int i = 0; i < 10; i++)
            {
                numero += r.Next(0, 9).ToString();
            }
            string result = numero;
            return result;
        }

    }
}
