using CapaDatos.Context;
using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Repositorios.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;

         public UsuarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task Actualizar(Usuario usuario)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ObtenerPorCorreo(string correo)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.Correo == correo);
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task Registrar(Usuario usuario)
        {
           _context.Usuario.Add(usuario);
           await _context.SaveChangesAsync();
        }

    }
}
