using CapaNegocio.DTOs.Auth;
using CapaNegocio.DTOs.DTOCreacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface ILoginService
    {
        Task Registrar(UsuarioCreateDTO usuario);
        Task Login(LoginDTO dto); 

    }
}
