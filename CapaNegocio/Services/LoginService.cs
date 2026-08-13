using BCrypt.Net;
using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.Auth;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepositorio _repo;
        private readonly ITokenService _tokenService;

        public LoginService(IUsuarioRepositorio repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }
        public async Task<string> Login(LoginDTO dto)
        {
            var usuario = await _repo.ObtenerPorCorreo(dto.Email);
            if (usuario == null)
            {
                throw new ValidacionException("Credencial incorrecta");
            }

            var clave = BCrypt.Net.BCrypt.Verify(dto.Clave, usuario.Clave);

            if (!clave)
            {
                throw new ValidacionException("Credencial no valida");
            }

            return _tokenService.GenerarToken(usuario);


        }

        public async Task<UsuarioDTO> Registrar(UsuarioCreateDTO dto)
        {
            var correo = await _repo.ObtenerPorCorreo(dto.Correo);
            if(correo != null)
            {
                throw new ValidacionException("Correo ya utilizado");
            }

            if(string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrEmpty(dto.Correo) || string.IsNullOrEmpty(dto.Clave))
            {
                throw new ValidacionException("Datos necesarios vacios");
            }

            var user = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Clave = BCrypt.Net.BCrypt.HashPassword(dto.Clave)
            };

            await _repo.Registrar(user);
            return MapearADTO(user);

        }

        private UsuarioDTO MapearADTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
            };
        }
    }
}
