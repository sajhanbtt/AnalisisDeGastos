using CapaDatos.Repositorios.Interfaces;
using CapaDatos.Repositorios.Repositories;
using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _repo;

        public UsuarioService(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

        public async Task Actualizar(int idUsuario, UsuarioUpdateDTO dto)
        {
            var user = await _repo.ObtenerPorId(idUsuario);
            if (user == null)
                throw new NotFoundException("Usuario no encontrado");

            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ValidacionException("El nombre no puede estar vacio");

            user.Nombre = dto.Nombre;

            if (!string.IsNullOrEmpty(dto.Clave))
            {
                user.Clave = BCrypt.Net.BCrypt.HashPassword(dto.Clave);
            }

            await _repo.Actualizar(user);

        }
    }
}
