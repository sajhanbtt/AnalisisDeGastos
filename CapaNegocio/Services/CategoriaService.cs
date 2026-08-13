using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        public async Task Actualizar(int id, CategoriaUpdateDTO dto, int idUsuario)
        {
            var categoria = await _repo.GetById(id);

            if (categoria == null || categoria.IdUsuario != idUsuario)
                throw new NotFoundException("Categoria no encontrada para actualizar");

            if (string.IsNullOrEmpty(dto.NombreCategoria))
                throw new ValidacionException("Nombre de categoria vacio");

            var categorias = await _repo.GetAllByUser(idUsuario);
            bool duplicado = categorias.Any(c =>c.Id != id && c.NombreCategoria.Equals(dto.NombreCategoria, StringComparison.OrdinalIgnoreCase));

            if (duplicado)
                throw new ValidacionException("Ya existe una categoría con ese nombre");

            categoria.NombreCategoria = dto.NombreCategoria;
            categoria.Descripcion = dto.Descripcion;
            categoria.Activo = dto.Activo;

            await _repo.Update(categoria);
        }

        public async Task<CategoriaDTO> Crear(CategoriaCreateDTO dto, int idUsuario)
        {

            if (string.IsNullOrEmpty(dto.NombreCategoria))
            {
                throw new ValidacionException("Nombre de categoria vacio");
            }

            var categorias = await _repo.GetAllByUser(idUsuario);
            if (categorias.Any(c => c.NombreCategoria.Equals(dto.NombreCategoria, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ValidacionException("Ya existe una categoría con ese nombre");
            }
                

            var categoria = new Categoria
            {
                NombreCategoria = dto.NombreCategoria,
                Descripcion = dto.Descripcion,
                Activo = true,
                IdUsuario = idUsuario
            };

            await _repo.Add(categoria);
            return MapearADto(categoria);

        }

        public async Task Eliminar(int id, int idUsuario)
        {
            var categoria = await _repo.GetById(id);

            if (categoria == null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria no encontrada para eliminar");
            }

            await _repo.Delete(categoria);

        }

        public async Task<List<CategoriaDTO>> Listar(int id)
        {
            var categoria = await _repo.GetAllByUser(id);

            var dto = categoria.Select(x => new CategoriaDTO
            {
                Id = x.Id,
                NombreCategoria = x.NombreCategoria,
                Descripcion = x.Descripcion,
                Activo = x.Activo

            }).ToList();

            return dto;
        }

        public async Task<CategoriaDTO> ObtenerPorId(int id, int idUsuario)
        {
            var categoria = await _repo.GetById(id);
            if (categoria == null || categoria.IdUsuario != idUsuario)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            var dto = new CategoriaDTO
            {
                Id = categoria.Id,
                NombreCategoria = categoria.NombreCategoria,
                Descripcion = categoria.Descripcion,
                Activo = categoria.Activo
            };

            return dto;

        }

        public CategoriaDTO MapearADto(Categoria categoria)
        {
            return new CategoriaDTO
            {
                Id = categoria.Id,
                NombreCategoria = categoria.NombreCategoria,
                Descripcion = categoria.Descripcion,
                Activo = categoria.Activo
            };
        }
    }
}
