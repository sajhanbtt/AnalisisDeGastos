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
    public class GastoService : IGastoService
    {
        private readonly IRepositorio<Gasto> _repo;
        private readonly IRepositorio<Categoria> _repoCategoria;
        private readonly IRepositorio<MetodoDePago> _repoMetodoPago;

        public GastoService(IRepositorio<Gasto> repo, IRepositorio<Categoria> repoCategoria, IRepositorio<MetodoDePago> repoMetodoPago)
        {
            _repo = repo;
            _repoCategoria= repoCategoria;
            _repoMetodoPago = repoMetodoPago;
        }
        public async Task Actualizar(int id,GastoUpdateDTO dto,int idUsuario)
        {
            var categoria = await _repoCategoria.GetById(dto.IdCategoria);
            var metodoPago = await _repoMetodoPago.GetById(dto.IdMetodoPago);
            var gasto = await _repo.GetById(id);

            if (dto.Monto <= 0)
            {
                throw new ValidacionException("El monto debe ser mayor a 0");
            }


            if (gasto == null || gasto.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Gasto no encontrado");
            }

            if (categoria == null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            if (metodoPago == null || metodoPago.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Metodo de pago no encontrado");
            }

            gasto.Monto = dto.Monto;
            gasto.Fecha = dto.Fecha;
            gasto.Descripcion = dto.Descripcion;
            gasto.IdCategoria = dto.IdCategoria;
            gasto.IdMetodoPago = dto.IdMetodoPago;

             await _repo.Update(gasto);

        }

        public async Task <GastoDTO> Crear(GastoCreateDTO dto, int idUsuario)
        {
            var categoria = await _repoCategoria.GetById(dto.IdCategoria);
            var metodoPago = await _repoMetodoPago.GetById(dto.IdMetodoPago);

            if(dto.Monto <= 0)
            {
                throw new ValidacionException("El monto debe ser mayor a 0");
            }

            if(categoria== null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            if (metodoPago == null || metodoPago.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Metodo de pago no encontrado");
            }


            var gasto = new Gasto
            {
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                IdCategoria = dto.IdCategoria,
                IdMetodoPago = dto.IdMetodoPago,
                IdUsuario = idUsuario
            };

            await _repo.Add(gasto);

            return MapearADto(gasto, categoria, metodoPago);
        }

        public async Task Eliminar(int id, int idUsuario)
        {
            var gasto = await _repo.GetById(id);

            if (gasto == null || gasto.IdUsuario != idUsuario)
            {
                throw new NotFoundException("No se encuentra el gasto para eliminar");

            }

            await _repo.Delete(gasto);
        }

        public async Task<List<GastoDTO>> Listar(int idUsuario)
        {
            var listaGasto = await _repo.GetAllByUser(idUsuario);
            var categorias = await _repoCategoria.GetAllByUser(idUsuario);
            var metodosPago = await _repoMetodoPago.GetAllByUser(idUsuario);

            var dto = listaGasto.Select(x => new GastoDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Fecha = x.Fecha,
                Descripcion = x.Descripcion,
                IdCategoria = x.IdCategoria,
                NombreCategoria = categorias.First(c => c.Id == x.IdCategoria).NombreCategoria,
                IdMetodoPago = x.IdMetodoPago,
                NombreMetodo = metodosPago.First(m => m.Id == x.IdMetodoPago).NombreMetodo
            }).ToList();

            return dto;
        }

        public async Task<GastoDTO> ObtenerPorId(int id, int idUsuario)
        {
            var gasto = await _repo.GetById(id);

            if (gasto== null || idUsuario != gasto.IdUsuario)   
            {
                throw new NotFoundException("Gasto no encontrado");
            }

            var categoria = await _repoCategoria.GetById(gasto.IdCategoria);
            var metodoPago = await _repoMetodoPago.GetById(gasto.IdMetodoPago);

            return MapearADto(gasto, categoria, metodoPago);
        }

        private GastoDTO MapearADto(Gasto gasto, Categoria categoria, MetodoDePago metodoPago)
        {
            return new GastoDTO
            {
                Id = gasto.Id,
                Monto = gasto.Monto,
                Fecha = gasto.Fecha,
                Descripcion = gasto.Descripcion,
                IdCategoria = gasto.IdCategoria,
                NombreCategoria = categoria.NombreCategoria,
                IdMetodoPago = gasto.IdMetodoPago,
                NombreMetodo = metodoPago.NombreMetodo
            };
        }

    }
}
