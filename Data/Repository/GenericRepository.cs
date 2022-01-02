using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
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

        public virtual async Task<T?> FindByIDAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<IActionResult> UpdateAsync(T entity, int id)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await FindByIDAsync(id) != null)
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw new Exception("Some think Went Wrong Try again later .");
                }
            }
            _context.Set<T>().Update(entity);
            return new OkResult();
        }

        public virtual async Task<List<T>> WhereAsync([NotNull] Expression<Func<T,bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T,bool>> predicate)
        {
            return await this._context.Set<T>().FirstOrDefaultAsync(predicate);
        }
    }
}
