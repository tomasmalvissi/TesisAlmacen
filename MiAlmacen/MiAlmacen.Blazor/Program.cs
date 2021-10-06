using MiAlmacen.Blazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44330/") });

            builder.Services.AddScoped<ClienteService>()
                            .AddScoped<ArticuloService>()
                            .AddScoped<ProveedorService>()
                            .AddScoped<UsuarioService>()
                            .AddScoped<VentaService>()
                            .AddScoped<CajaService>()
                            .AddScoped<FormasPagoService>();

            await builder.Build().RunAsync();
        }
    }
}
