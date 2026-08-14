using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Excepciones;
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

        public async Task Actualizar(int id,MetodoDePagoUpdateDTO dto, int idUsuario)
        {
            var metodoPago = await _repo.GetById(id);


            if(metodoPago == null || metodoPago.IdUsuario != idUsuario)
            {
                throw new NotFoundException("No se ha encontrado el metodo de pago");
            }

            if (string.IsNullOrEmpty(dto.NombreMetodo))
            {
                throw new ValidacionException("Nombre de Metodo de pago vacio");
            }

            var metodosPago = await _repo.GetAllByUser(idUsuario);

            if (metodosPago.Any(m => m.Id != id && m.NombreMetodo.Equals(dto.NombreMetodo, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ValidacionException("Ya existe un metodo de pago con ese nombre");
            }

            metodoPago.NombreMetodo = dto.NombreMetodo;
            metodoPago.Descripcion = dto.Descripcion;
            metodoPago.Icono = dto.Icono;
            metodoPago.Activo = dto.Activo;


            await _repo.Update(metodoPago);
        }

        public async Task<MetodoDePagoDTO> Crear(MetodoDePagoCreateDTO dto, int idUsuario)
        {
            if (string.IsNullOrEmpty(dto.NombreMetodo))
            {
                throw new ValidacionException("Nombre de Metodo de pago vacio");
            }

            var metodosPago = await _repo.GetAllByUser(idUsuario);

            if (metodosPago.Any(m => m.NombreMetodo.Equals(dto.NombreMetodo, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ValidacionException("Ya existe un metodo de pago con ese nombre");
            }

            var metodo = new MetodoDePago
            {
                NombreMetodo = dto.NombreMetodo,
                Descripcion = dto.Descripcion,
                Icono = dto.Icono,
                Activo = true,
                IdUsuario = idUsuario

            };

            await _repo.Add(metodo);
            return MapearADto(metodo);
        }

        public async Task Eliminar(int id, int idUsuario)
        {
            var metodoPago = await _repo.GetById(id);

            if (metodoPago == null || metodoPago.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Metodo de pago no encontrado");
            }

            await _repo.Delete(metodoPago);

        }

        public async Task<List<MetodoDePagoDTO>> Listar(int idUsuario)
        {
            var metodosPago = await _repo.GetAllByUser(idUsuario);

            var dto = metodosPago.Select(x => new MetodoDePagoDTO
            {
                Id = x.Id,
                NombreMetodo = x.NombreMetodo,
                Descripcion = x.Descripcion,
                Icono = x.Icono,
                Activo = x.Activo

            }).ToList();

            return dto;
        }

        public async Task<MetodoDePagoDTO> ObtenerPorId(int id, int idUsuario)
        {
            var metodoPago = await _repo.GetById(id);

            if(metodoPago == null || metodoPago.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Metodo de pago no encontrado");
            }

            return MapearADto(metodoPago);
        }

        private MetodoDePagoDTO MapearADto(MetodoDePago metodoPago)
        {
            return new MetodoDePagoDTO
            {
                Id = metodoPago.Id,
                NombreMetodo = metodoPago.NombreMetodo,
                Descripcion = metodoPago.Descripcion,
                Icono = metodoPago.Icono,
                Activo = metodoPago.Activo
            };
        }
    }
}
