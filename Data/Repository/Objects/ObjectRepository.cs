using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using webApp.Manager;
using Object = webApp.Models.Object;


namespace webApp.Data.Repository.Objects
{
    public class ObjectRepository : GenericRepository<Object>, IObjectRepository
    {
        public ObjectRepository(ShopContext context) : base(context)
        {
        }

        public override async Task<List<Object>> AllAsync()
        {
            return await this._context.Objects.Include(o => o.SubCategori).Include(o => o.Attribute).AsNoTracking().ToListAsync();
        }
        
        public IQueryable<Object> AllQuery()
        {
            return this._context.Objects.Include(o => o.SubCategori).Include(o => o.Attribute).AsNoTracking();
        }

        public IQueryable<Object> WhereQuery([NotNull] Expression<Func<Object,bool>> predicate)
        {
            return this._context.Objects.Include(o => o.SubCategori).Include(o => o.Attribute).Where(predicate).AsNoTracking();
        }

        public override Task<Object?> FindByIDAsync(int? id)
        {
            return this._context.Objects.Include(o => o.SubCategori).Include(o => o.Attribute).FirstOrDefaultAsync(o=> o.ObjectID == id);
        }

        public async ValueTask<EntityEntry<Object>> AddAsync(Object entity, IFormFile file,
            IWebHostEnvironment Environment)
        {
            entity.image = FileManager.storeAs(file, FileManager.FileStorePAth.img, Environment);
            return await base.AddAsync(entity);
        }

        public void Remove(Object entity, IWebHostEnvironment Environment)
        {
            if (entity.image != null)
                FileManager.DeleteFile(entity.image, FileManager.FileStorePAth.img, Environment);
            base.Remove(entity);
        }


        public async Task<IActionResult> UpdateAsync(Object entity, int id, IFormFile file, IWebHostEnvironment Environment)
        {
            FileManager.DeleteFile((await this._context.Objects!.AsNoTracking().FirstOrDefaultAsync(o=>o.ObjectID == id))!.image!, FileManager.FileStorePAth.img, Environment);
            entity.image = FileManager.storeAs(file, FileManager.FileStorePAth.img, Environment);
            return await base.UpdateAsync(entity, id);
        }
    }
}