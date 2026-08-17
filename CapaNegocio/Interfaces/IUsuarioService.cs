using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IUsuarioService
    {
        Task Actualizar(int idUsuario,UsuarioUpdateDTO usuario);
    }
}
