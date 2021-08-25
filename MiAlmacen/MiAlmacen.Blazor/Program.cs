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
            //TODO: antes de ejecutar chequear que service llame a nuestro localhost

            /*MAURI*/
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44368/") });

            /*TOMY*/
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44330/") });

            builder.Services.AddScoped<IClienteService, ClienteService>();

            await builder.Build().RunAsync();
        }
    }
}
