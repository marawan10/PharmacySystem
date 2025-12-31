using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
