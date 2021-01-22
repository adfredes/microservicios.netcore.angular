using Servicios.api.seguridad.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.seguridad.Core.JwtLogic
{
    public interface IJwtGenerator
    {
        string CreateToken(Usuario usuario);
    }
}
