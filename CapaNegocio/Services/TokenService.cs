using CapaNegocio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CapaNegocio.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GenerarToken(string correo, string clave)
        {
            var key = _config["Jwt : Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credenciales = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity([

                    new Claim(ClaimTypes.Name, correo),
                    new Claim(ClaimTypes.Role, "Admin")

                    ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt : TokenExpireInMinutes"])),
                SigningCredentials = credenciales,
                Issuer = _config["Jwt: Issuer"],
                Audience = _config["Jwt : Audience"]
            };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);

            return token;

        }
    }
}
