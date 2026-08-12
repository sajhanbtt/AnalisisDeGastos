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
    public class MetodoDePagoService : IMetodoPagoService
    {
        private readonly IRepositorio<MetodoDePago> _repo;

        public MetodoDePagoService(IRepositorio<MetodoDePago> repo)
        {
            _repo = repo;
        }
        public Task Actualizar(MetodoDePagoUpdateDTO metodoPago)
        {
            throw new NotImplementedException();
        }

        public Task Crear(MetodoDePagoCreateDTO metodoPago)
        {
            throw new NotImplementedException();
        }

        public Task Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PresupuestoDTO>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<PresupuestoDTO> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
