using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    internal class LoginService : ILoginService
    {
        public Task Login(string correo, string clave)
        {
            throw new NotImplementedException();
        }

        public Task Registrar(UsuarioCreateDTO usuario)
        {
            throw new NotImplementedException();
        }
    }
}
