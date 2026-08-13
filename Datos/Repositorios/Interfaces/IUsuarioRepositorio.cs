using CapaEntidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task Registrar(Usuario usuario);
        Task<Usuario> ObtenerPorCorreo(string correo); 
    }
}
