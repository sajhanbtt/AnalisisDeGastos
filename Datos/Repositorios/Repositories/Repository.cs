using CapaDatos.Context;
using CapaDatos.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Repositorios.Repositories
{
    public class Repository<T> : IRepositorio<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task Add(T model)
        {
             _dbSet.Add(model);
             await _context.SaveChangesAsync();
        }

        public async Task Delete(T model)
        {
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllByUser(int id)
        {
            return await _dbSet.Where(x=>EF.Property<int>(x, "IdUsuario") == id).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T model)
        {
            await _context.SaveChangesAsync();
        }
    }
}
