using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Servicios.api.seguridad.Core.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.api.seguridad.Core.JwtLogic
{
    public class JwtGenerator : IJwtGenerator
    {
        public JwtGenerator()
        {
        }

        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("iM5D6MrA00bdKN2RboXtR7Cx94qnfjtj"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = credential
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDesc);

            return tokenhandler.WriteToken(token);

            //https://randomkeygen.com/
        }
    }
}
