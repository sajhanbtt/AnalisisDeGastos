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

        public async Task Crear(CategoriaCreateDTO categoria)
        {
            if (string.IsNullOrEmpty(categoria.NombreCategoria))
            {
                throw new ValidacionException("Nombre de categoria vacio");
            }

            await _repo.Add();


        }

        public async Task Eliminar(int id)
        {
            var categoria = await ObtenerPorId(id);
            await _repo.Delete(categoria);
    
        }

        public async Task<List<CategoriaDTO>> Listar()
        {
            var categoria = await _repo.GetAll();
            return categoria;
        }

        public async Task<CategoriaDTO> ObtenerPorId(int id)
        {
            var categoria = await _repo.GetById(id);
            if (categoria == null)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            return categoria;

        }
    }
}
