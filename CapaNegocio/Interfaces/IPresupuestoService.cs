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
        Task Crear(PresupuestoCreateDTO presupuesto);
        Task<List<PresupuestoDTO>> Listar();

        Task<PresupuestoDTO> ObtenerPorId(int id);

        Task Actualizar(PresupuestoUpdateDTO presupuesto);

        Task Eliminar(int id);
    }
}
