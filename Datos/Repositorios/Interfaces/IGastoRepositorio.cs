using CapaEntidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Repositorios.Interfaces
{
    public interface IGastoRepositorio
    {
        Task<List<Gasto>> GetAllByUser(int id);
        Task<Gasto> GetById(int id);

        Task Add(Gasto gasto);

        Task Update(Gasto gasto);

        Task Delete(Gasto gasto);

        Task InsertMasivo(IEnumerable<Gasto> gastos);
    }
}
