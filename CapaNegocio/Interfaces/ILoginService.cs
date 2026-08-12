using CapaNegocio.DTOs.DTOCreacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface ILoginService
    {
        Task Registrar(UsuarioCreateDTO usuario);
        Task Login(string correo, string clave); 

    }
}
