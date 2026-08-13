using CapaNegocio.DTOs.Auth;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface ILoginService
    {
        Task<UsuarioDTO> Registrar(UsuarioCreateDTO usuario);
        Task<string> Login(LoginDTO dto); 

    }
}
