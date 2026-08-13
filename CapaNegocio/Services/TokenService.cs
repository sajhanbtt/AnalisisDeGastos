using CapaEntidades.Models;
using CapaNegocio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {

                new Claim (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };

            var SecretKey = _config["Jwt:SecretKey"];
            var Issuer = _config["Jwt:Issuer"];
            var Audience = _config["Jwt:Audience"];

            var Expiration = int.Parse(_config["Jwt:ExpireInMinutes"]!);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: Issuer, audience: Audience, claims: claims, expires: DateTime.UtcNow.AddMinutes(Expiration),signingCredentials: credenciales);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
