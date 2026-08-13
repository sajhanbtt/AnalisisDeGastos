using CapaEntidades.Models;
using CapaNegocio.DTOs.Auth;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    public class LoginService : ILoginService
    {
        public async Task Login(LoginDTO dto)
        {
            
        }

        public Task Registrar(UsuarioCreateDTO dto)
        {
            if(string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrEmpty(dto.Correo) || string.IsNullOrEmpty(dto.Clave))
            {
                throw new ValidacionException("Datos necesarios vacios");
            }



            var user = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Clave = dto.Clave

            };


        }
    }
}
