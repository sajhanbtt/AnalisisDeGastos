using CapaEntidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}
