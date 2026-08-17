using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IPresupuestoService
    {
        Task<PresupuestoDTO> Crear(PresupuestoCreateDTO presupuesto, int idUsuario);
        Task<List<PresupuestoDTO>> Listar(int idUsuario);

        Task<PresupuestoDTO> ObtenerPorId(int id, int idUsuario);

        Task Actualizar(int id,PresupuestoUpdateDTO presupuesto, int idUsuario);

        Task Eliminar(int id, int idUsuario);
    }
}

