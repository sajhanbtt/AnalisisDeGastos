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
        Task Crear(CategoriaCreateDTO categoria);
        Task<List<CategoriaDTO>> Listar();

        Task<CategoriaDTO> ObtenerPorId(int id);

        Task Actualizar(int id, CategoriaUpdateDTO categoria);

        Task Eliminar(int id);

    }
}
