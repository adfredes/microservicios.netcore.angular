using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Servicios.api.seguridad.Core.Entity;
using Servicios.api.seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.seguridad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var hostServer = CreateHostBuilder(args).Build();
            using var contexto = hostServer.Services.CreateScope();
            var service = contexto.ServiceProvider;
            try
            {
                var userManager = service.GetRequiredService<UserManager<Usuario>>();
                var contextEf = service.GetRequiredService<SeguridadContexto>();
                SeguridadData.InsertarUsuario(contextEf, userManager).Wait();
            }
            catch
            {

            }
            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
