using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerarToken(string correo, string clave);
    }
}
