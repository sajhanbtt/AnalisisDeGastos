using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
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

        public async Task Actualizar(int id, CategoriaUpdateDTO dto)
        {
            var categoria = await _repo.GetById(id);

            if (categoria == null)
            {
                throw new NotFoundException("Categoria no encontrada para actualizar");
            }

            if (string.IsNullOrEmpty(dto.NombreCategoria))
            {
                throw new ValidacionException("Nombre de categoria vacio");
            }

            categoria.NombreCategoria = dto.NombreCategoria;
            categoria.Descripcion = dto.Descripcion;
            categoria.Activo = dto.Activo;
            

        }

        public async Task Crear(CategoriaCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.NombreCategoria))
            {
                throw new ValidacionException("Nombre de categoria vacio");
            }

            var categoria = new Categoria
            {
                NombreCategoria = dto.NombreCategoria,
                Descripcion = dto.Descripcion,
                IdUsuario = dto.IdUsuario
            };

            await _repo.Add(categoria);

        }

        public async Task Eliminar(int id)
        {
            var categoria = await _repo.GetById(id);

            if(categoria == null)
            {
                throw new NotFoundException("Categoria no encontrada para eliminar");
            }

            await _repo.Delete(categoria);
    
        }

        public async Task<List<CategoriaDTO>> Listar()
        {
            var categoria = await _repo.GetAll();

            var dto = categoria.Select(x => new CategoriaDTO
            {
                Id = x.Id,
                NombreCategoria = x.NombreCategoria,
                Descripcion = x.Descripcion,
                Activo = x.Activo
               
            }).ToList();

            return dto;
        }

        public async Task<CategoriaDTO> ObtenerPorId(int id)
        {
            var categoria = await _repo.GetById(id);
            if (categoria == null)
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
    }
}
