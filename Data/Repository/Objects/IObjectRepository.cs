using Microsoft.EntityFrameworkCore.ChangeTracking;
using Object = webApp.Models.Object;

namespace webApp.Data.Repository.Objects
{
    public interface IObjectRepository : IGenericRepository<Object>
    {
        public ValueTask<EntityEntry<Object>> AddAsync(Object entity, IFormFile file, IWebHostEnvironment Environment);

        public void Remove(Object entity, IWebHostEnvironment Environment);

        public void UpdateAsync(Object entity, int id, IFormFile file, IWebHostEnvironment Environment);
    }
}
