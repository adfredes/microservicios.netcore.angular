using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Servicios.api.seguridad.Core.JwtLogic
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor httpContext;

        public UsuarioSesion(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUsuarioSesion()
        {
            
            return this.httpContext.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value; ;
        }
    }
}
