using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepositorio _repo;
        private readonly IRepositorio<Categoria> _repoCategoria;
        private readonly IRepositorio<MetodoDePago> _repoMetodoPago;

        public GastoService(IGastoRepositorio repo, IRepositorio<Categoria> repoCategoria, IRepositorio<MetodoDePago> repoMetodoPago)
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

        public async Task<ImportacionDTO> ImportarArchivo(IFormFile archivo, int idUsuario)
        {
            if (archivo == null || archivo.Length == 0)
                throw new ValidacionException("El archivo está vacío o no es válido.");

            var categorias = await _repoCategoria.GetAllByUser(idUsuario);
            var metodosPago = await _repoMetodoPago.GetAllByUser(idUsuario);

            var gastosValidos = new List<Gasto>();
            var errores = new List<string>();

            using var stream = archivo.OpenReadStream();
            using var workbook = new XLWorkbook(stream);
            var filas = workbook.Worksheet(1).RowsUsed().Skip(1);

            foreach (var fila in filas)
            {
                int numeroFila = fila.RowNumber();

                bool montoValido = decimal.TryParse(fila.Cell(2).GetValue<string>(), out decimal monto);
                if (!montoValido || monto <= 0)
                {
                    errores.Add($"Fila {numeroFila}: monto inválido");
                    continue;
                }

                string nombreCategoria = fila.Cell(3).GetValue<string>().Trim();
                var categoria = categorias.FirstOrDefault(c => c.NombreCategoria.Equals(nombreCategoria, StringComparison.OrdinalIgnoreCase));
                if (categoria == null)
                {
                    errores.Add($"Fila {numeroFila}: categoría '{nombreCategoria}' no encontrada");
                    continue;
                }

                string nombreMetodo = fila.Cell(4).GetValue<string>().Trim();
                var metodo = metodosPago.FirstOrDefault(m => m.NombreMetodo.Equals(nombreMetodo, StringComparison.OrdinalIgnoreCase));
                if (metodo == null)
                {
                    errores.Add($"Fila {numeroFila}: método de pago '{nombreMetodo}' no encontrado");
                    continue;
                }

                gastosValidos.Add(new Gasto
                {
                    Descripcion = fila.Cell(1).GetValue<string>(),
                    Monto = monto,
                    IdCategoria = categoria.Id,
                    IdMetodoPago = metodo.Id,
                    Fecha = DateTime.Now,
                    IdUsuario = idUsuario
                });
            }

            if (gastosValidos.Count > 0) await _repo.InsertMasivo(gastosValidos);

            return new ImportacionDTO
            {
                FilasExitosas = gastosValidos.Count,
                Errores = errores
            };
        }
        public async Task<ReporteDTO> ObtenerReporteMensual(int mes, int anio, int idUsuario)
        {
            if (mes < 1 || mes > 12)
                throw new ValidacionException("Mes inválido");

            if (anio < 2000 || anio > DateTime.Now.Year)
                throw new ValidacionException("Año inválido");

            var todosLosGastos = await _repo.GetAllByUser(idUsuario);
            var categorias = await _repoCategoria.GetAllByUser(idUsuario);

            var gastosDelMes = todosLosGastos
                .Where(g => g.Fecha.Month == mes && g.Fecha.Year == anio)
                .ToList();

            int mesAnterior = mes - 1;
            int anioAnterior = anio;
            if (mesAnterior == 0)
            {
                mesAnterior = 12;
                anioAnterior = anio - 1;
            }

            var gastosDelMesAnterior = todosLosGastos
                .Where(g => g.Fecha.Month == mesAnterior && g.Fecha.Year == anioAnterior)
                .ToList();

            decimal totalGastado = gastosDelMes.Sum(g => g.Monto);
            decimal totalMesAnterior = gastosDelMesAnterior.Sum(g => g.Monto);

            var desglose = gastosDelMes
                .GroupBy(g => g.IdCategoria)
                .Select(grupo => new DesgloseCategoriaDTO
                {
                    NombreCategoria = categorias.First(c => c.Id == grupo.Key).NombreCategoria,
                    MontoTotal = grupo.Sum(g => g.Monto)
                })
                .ToList();

            var topCategorias = desglose
                .OrderByDescending(d => d.MontoTotal)
                .Take(3)
                .ToList();

            return new ReporteDTO
            {
                Mes = mes,
                Anio = anio,
                TotalGastado = totalGastado,
                TotalMesAnterior = totalMesAnterior,
                DiferenciaConMesAnterior = totalGastado - totalMesAnterior,
                DesglosePorCategoria = desglose,
                TopCategorias = topCategorias
            };
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
