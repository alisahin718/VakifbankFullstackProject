using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> 
        where T : class
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            var addedEntity = _context.Add(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                    ? await _context.Set<T>().ToListAsync()
                    : await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task Update(T entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
    
}
