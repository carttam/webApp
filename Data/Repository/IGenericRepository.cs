using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace webApp.Data.Repository
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T?> FindByIDAsync(int? id);
        Task<List<T>> AllAsync();
        ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> AddAsync(T entity);
        void Remove(T entity);
        Task<IActionResult> UpdateAsync(T entity, int id);
        Task<List<T>> Where([NotNull] Expression<Func<T,bool>> predicate);
        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
