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
    public class GastoService : IGastoService
    {
        private readonly IRepositorio<Gasto> _repo;

        public GastoService(IRepositorio<Gasto> repo)
        {
            _repo = repo;
        }
        public Task Actualizar(GastoUpdateDTO gasto)
        {
            throw new NotImplementedException();
        }

        public Task Crear(GastoCreateDTO gasto)
        {
            throw new NotImplementedException();
        }

        public Task Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GastoDTO>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<GastoDTO> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
