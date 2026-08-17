using CapaDatos.Context;
using CapaDatos.Repositorios.Interfaces;
using CapaEntidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Repositorios.Repositories
{
    public class GastoRepositorio : IGastoRepositorio
    {
        private readonly AppDbContext _context;

        public GastoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Gasto gasto)
        {
            _context.Gasto.Add(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Gasto gasto)
        {
            _context.Gasto.Remove(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Gasto>> GetAllByUser(int id)
        {
            return await _context.Gasto.Where(x => x.IdUsuario == id).ToListAsync();
        }

        public async Task<Gasto> GetById(int id)
        {
            return await _context.Gasto.FindAsync(id);
        }

        public async Task InsertMasivo(IEnumerable<Gasto> gastos)
        {
            await _context.Gasto.AddRangeAsync(gastos);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Gasto gasto)
        {
            await _context.SaveChangesAsync();
        }
    }
}
