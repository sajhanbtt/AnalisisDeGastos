using CapaEntidades.Models;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> db) : base(db)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<MetodoDePago> MetodoDePago { get; set; }
        public DbSet<Gasto> Gasto { get; set; }
        public DbSet<Presupuesto> Presupuesto { get; set; }

    }
}
