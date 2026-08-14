using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDTO> Crear(CategoriaCreateDTO categoria, int idUsuario);
        Task<List<CategoriaDTO>> Listar(int id);

        Task<CategoriaDTO> ObtenerPorId(int id, int idUsuario);

        Task Actualizar(int id, CategoriaUpdateDTO categoria, int idUsuario);

        Task Eliminar(int id, int idUsuario, int? idReasignacion);

    }
}
