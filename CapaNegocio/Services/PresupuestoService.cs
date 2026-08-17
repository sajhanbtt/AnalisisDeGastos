using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaNegocio.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IRepositorio<Presupuesto> _repo;
        private readonly IRepositorio<Categoria> _repoCategoria;

        private readonly IGastoRepositorio _repoGasto;

        public PresupuestoService(IRepositorio<Presupuesto> repo, IRepositorio<Categoria> repoCategoria, IGastoRepositorio repoGasto)
        {
            _repo = repo;
            _repoCategoria = repoCategoria;
            _repoGasto = repoGasto;
        }
        public async Task<PresupuestoDTO> Crear(PresupuestoCreateDTO dto, int idUsuario)
        {
            if(dto.Monto<= 0)
            {
                throw new ValidacionException("Monto no valido");
            }

            if (dto.Mes < 1 || dto.Mes > 12)
            {
                throw new ValidacionException("Mes ingresado no valido");
            }

            if(dto.Anio < 2026 || dto.Anio > 2050)
            {
                throw new ValidacionException("Año introducido no valido de momento");
            }

            var categoria = await _repoCategoria.GetById(dto.IdCategoria);

            if (categoria == null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            var listaPresupuesto = await _repo.GetAllByUser(idUsuario);

            bool presupuestoPrevio = listaPresupuesto.Any(p => p.Mes == dto.Mes && p.Anio == dto.Anio && p.IdCategoria == dto.IdCategoria);

            if (presupuestoPrevio)
            {
                throw new ValidacionException("Ya hay un presupuesto almacenado con la misma categoria, mes y año");
            }

            var presupuesto = new Presupuesto
            {
                  Mes = dto.Mes,
                  Anio = dto.Anio,
                  Monto = dto.Monto,
                  IdCategoria = dto.IdCategoria,
                  IdUsuario= idUsuario,

            };

            await _repo.Add(presupuesto);
            return await MapearADto(presupuesto, categoria, idUsuario);

        }

        public async Task Actualizar(int id,PresupuestoUpdateDTO dto, int idUsuario)
        {
            var presupuesto = await _repo.GetById(id);

            if(presupuesto== null || presupuesto.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Presupuesto no encontrado");
            }

            if (dto.Monto <= 0)
            {
                throw new ValidacionException("Monto no valido");
            }

            if (dto.Mes < 1 || dto.Mes > 12)
            {
                throw new ValidacionException("Mes ingresado no valido");
            }

            if (dto.Anio < 2026 || dto.Anio > 2050)
            {
                throw new ValidacionException("Año introducido no valido de momento");
            }

            var categoria = await _repoCategoria.GetById(dto.IdCategoria);

            if (categoria == null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            var listaPresupuesto = await _repo.GetAllByUser(idUsuario);

            bool presupuestoPrevio = listaPresupuesto.Any(p => p.Id != id && p.Mes == dto.Mes && p.Anio == dto.Anio && p.IdCategoria == dto.IdCategoria);

            if (presupuestoPrevio)
            {
                throw new ValidacionException("Ya hay un presupuesto almacenado con la misma categoria, mes y año");
            }

            presupuesto.Mes = dto.Mes;
            presupuesto.Anio = dto.Anio;
            presupuesto.Monto = dto.Monto;
            presupuesto.IdCategoria = dto.IdCategoria;
            

            await _repo.Update(presupuesto);
           
        }

        public async Task Eliminar(int id, int idUsuario)
        {
            var presupuesto = await _repo.GetById(id);

            if(presupuesto == null || presupuesto.IdUsuario != idUsuario)
            {
                throw new NotFoundException("No se encontro ningun elemento para eliminar");

            }

            await _repo.Delete(presupuesto);
        }

        public async Task<List<PresupuestoDTO>> Listar(int idUsuario)
        {
            var presupuestos = await _repo.GetAllByUser(idUsuario);
            var categorias = await _repoCategoria.GetAllByUser(idUsuario); 

            var lista = new List<PresupuestoDTO>();

            foreach (var p in presupuestos)
            {
                var categoria = categorias.First(c => c.Id == p.IdCategoria); 
                var dto = await MapearADto(p, categoria, idUsuario);
                lista.Add(dto);
            }

            return lista;
        }

        public async Task<PresupuestoDTO> ObtenerPorId(int id, int idUsuario)
        {
            var presupuesto = await _repo.GetById(id);
            if (presupuesto == null || presupuesto.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Presupuesto no encontrado");
            }

            var categoria = await _repoCategoria.GetById(presupuesto.IdCategoria);

            if (categoria == null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria asociada no encontrada");
            }

            return await MapearADto(presupuesto,categoria,idUsuario);

        }

        private async Task<PresupuestoDTO> MapearADto(Presupuesto presupuesto,Categoria categoria, int idUsuario)
        {
            var gastos = await _repoGasto.GetAllByUser(idUsuario);

            var gastoAcumulado = gastos.Where(g=> g.IdCategoria == categoria.Id && g.Fecha.Month == presupuesto.Mes && g.Fecha.Year == presupuesto.Anio).Sum(g=> g.Monto);

            var porcentaje = (gastoAcumulado/presupuesto.Monto) * 100;

            string alerta;

            if (porcentaje >= 100)
            {
                alerta = "Presupuesto excedido";
            }
            else if (porcentaje >= 80)
            {
                alerta = "Alerta: 80% del presupuesto consumido";
            }
            else if (porcentaje >= 50)
            {
                alerta = "Alerta: 50% del presupuesto consumido";
            }
            else
            {
                alerta = "Sin alerta";
            }

            return new PresupuestoDTO
            {
                Id = presupuesto.Id,
                Mes = presupuesto.Mes,
                Anio = presupuesto.Anio,
                Monto = presupuesto.Monto,
                IdCategoria = presupuesto.IdCategoria,
                NombreCategoria = categoria.NombreCategoria,
                GastoAcumulado = gastoAcumulado,
                PorcentajeConsumido = porcentaje,
                Alerta = alerta

            };
        }
    }
}
