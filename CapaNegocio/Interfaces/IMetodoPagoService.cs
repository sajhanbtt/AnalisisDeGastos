using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IMetodoPagoService
    {
        Task Crear(MetodoDePagoCreateDTO metodoPago);
        Task<List<PresupuestoDTO>> Listar();

        Task<PresupuestoDTO> ObtenerPorId(int id);

        Task Actualizar(MetodoDePagoUpdateDTO metodoPago);

        Task Eliminar(int id);
    }
}
