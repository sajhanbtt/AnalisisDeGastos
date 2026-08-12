using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IRepositorio<Categoria> _repo;

        public CategoriaService(IRepositorio<Categoria> repo)
        {
            _repo = repo;
        }

        public Task Actualizar(CategoriaUpdateDTO categoria)
        {
            throw new NotImplementedException();
        }

        public Task Crear(CategoriaCreateDTO categoria)
        {
            throw new NotImplementedException();
        }

        public Task Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoriaDTO>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaDTO> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
