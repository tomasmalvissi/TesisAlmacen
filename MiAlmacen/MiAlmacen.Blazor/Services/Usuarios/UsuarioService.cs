using MiAlmacen.Model.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
            var respuesta = _httpClient.GetStringAsync($"api/usuarios/{id}");
            return JsonConvert.DeserializeObject<UsuarioModel>(await respuesta);
        }

        public async Task<UsuarioModel> GetUser(string usuario)
        {
            var respuesta = _httpClient.GetStringAsync($"api/usuarios/get/{usuario}");
            return JsonConvert.DeserializeObject<UsuarioModel>(await respuesta);
        }

        public async Task<HttpResponseMessage> Alta(UsuarioModel usuario)
        {
            string userSerealizado = JsonConvert.SerializeObject(usuario);
            var respuesta = await _httpClient.PostAsync("api/usuarios/",
                      new StringContent(userSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
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
            if (!respuesta.IsSuccessStatusCode) id = 0;
            return id;
        }


        //LOGIN SERVICE
        public async Task<UsuarioModel> Login(string username, string pass)
        {
            var respuesta = await _httpClient.GetAsync($"api/usuarios/{username}/{pass}");
            UsuarioModel usuario = new();
            if (respuesta.IsSuccessStatusCode)
            {
                var obj = respuesta.Content.ReadAsStringAsync();
                int id = JsonConvert.DeserializeObject<int>(await obj);
                if (id.Equals(0))
                    return null;
                else
                {
                    usuario = await GetUn(id);
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "Id", Encriptar(usuario.Id.ToString()));
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "Nombre", Encriptar(usuario.Nombre));
                }
            }
            return usuario;
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "Id");
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "Nombre");
        }

        public async Task<UsuarioModel> GetSesion()
        {
            UsuarioModel usuario;
            string id = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "Id");
            string nombre = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "Nombre");
            if (id == null)
                return null;
            else
            {
                usuario = new UsuarioModel()
                {
                    Id = Convert.ToInt32(DesEncriptar(id)),
                    Nombre = DesEncriptar(nombre)
                };
                return usuario;
            }
        }

        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
