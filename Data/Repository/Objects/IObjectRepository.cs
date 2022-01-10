using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Object = webApp.Models.Object;

namespace webApp.Data.Repository.Objects
{
    public interface IObjectRepository : IGenericRepository<Object>
    {
        public ValueTask<EntityEntry<Object>> AddAsync(Object entity, IFormFile file, IWebHostEnvironment Environment);

        public void Remove(Object entity, IWebHostEnvironment Environment);

        public Task<IActionResult> UpdateAsync(Object entity, int id, IFormFile file, IWebHostEnvironment Environment);
        public new Task<Object?> FindByIDAsync(int? id);
        public IQueryable<Object> AllQuery();
        public IQueryable<Object> WhereQuery([NotNull] Expression<Func<Object, bool>> predicate);
    }
}
