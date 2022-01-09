using LoginCoralisAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly LoginContext _context;
        public GenericRepository(LoginContext context) 
        {
            _context = context;
        }
        public void create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void createRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void deleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async  Task<bool> saveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
