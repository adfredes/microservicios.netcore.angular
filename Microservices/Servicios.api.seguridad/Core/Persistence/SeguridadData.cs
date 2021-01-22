using Microsoft.AspNetCore.Identity;
using Servicios.api.seguridad.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.seguridad.Core.Persistence
{
    public class SeguridadData
    {
        public static async Task InsertarUsuario(SeguridadContexto context, UserManager<Usuario> userManager)
        {
            if (userManager.Users.Any()) return;

            var usuario = new Usuario
            {
                Nombre = "Alejando",
                Apellido = "Fredes",
                Direccion = "Mi Casa",
                UserName = "adfredes",
                Email = "adfredes@gmail.com"
            };
            var result = await userManager.CreateAsync(usuario, "Pa$$w0rd");

        }
    }
}
