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
        Task<MetodoDePagoDTO> Crear(MetodoDePagoCreateDTO metodoPago, int idUsuario);
        Task<List<MetodoDePagoDTO>> Listar(int idUsuario);

        Task<MetodoDePagoDTO> ObtenerPorId(int id, int idUsuario);

        Task Actualizar(int id, MetodoDePagoUpdateDTO metodoPago, int idUsuario);

        Task Eliminar(int id, int idUsuario);
    }
}
