using Microsoft.EntityFrameworkCore;
using PharmacySystem.Application.Interfaces.Repositories;
using PharmacySystem.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Infrastructure.Repositories
{
    public class GenericRepository<T> (ApplicationDbContext _context) : IGenericRepository<T> where T : class
    {
        protected  ApplicationDbContext _context = _context;
        protected  DbSet<T> _dbSet = _context.Set<T>();

        public void Add(T entity) => _context.Add(entity);
        public void Delete(T entity) => _context.Remove(entity);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
