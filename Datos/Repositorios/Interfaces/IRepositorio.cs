using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Repositorios.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task<List<T>> GetAllByUser(int id);
        Task<T> GetById(int id);

        Task Add(T model);

        Task Update(int id, T model);

        Task Delete(T model);
    }
}
