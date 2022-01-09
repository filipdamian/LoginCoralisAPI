using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity>
    {
        //get data
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        //create
        void create(TEntity entity);
        void createRange(IEnumerable<TEntity> entities);
        //update
        void update(TEntity entity);
        //delete
        void delete(TEntity entity);
        void deleteRange(IEnumerable<TEntity> entities);
        //save
        Task<bool>saveAsync();
    }
}
