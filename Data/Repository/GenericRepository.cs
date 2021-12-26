using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace webApp.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ShopContext _context;

        public GenericRepository(ShopContext context)
        {
            _context = context;
        }

        public virtual async ValueTask<EntityEntry<T>> AddAsync(T entity)
        {
            return await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task<List<T>> AllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> FindByIDAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
