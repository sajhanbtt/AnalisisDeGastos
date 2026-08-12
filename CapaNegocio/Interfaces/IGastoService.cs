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
        Task Crear(GastoCreateDTO gasto);
        Task<List<GastoDTO>> Listar();

        Task<GastoDTO> ObtenerPorId(int id);

        Task Actualizar(GastoUpdateDTO gasto);

        Task Eliminar(int id);
    }
}
