using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IGastoService
    {
        Task<GastoDTO> Crear(GastoCreateDTO gasto, int idUsuario);
        Task<List<GastoDTO>> Listar(int idUsuario);

        Task<GastoDTO> ObtenerPorId(int id, int idUsuario);

        Task Actualizar(int id,GastoUpdateDTO gasto, int idUsuario);

        Task Eliminar(int id, int idUsuario);
    }
}
