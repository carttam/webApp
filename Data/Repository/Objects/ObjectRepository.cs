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


        public async void UpdateAsync(Object entity, int id, IFormFile file, IWebHostEnvironment Environment)
        {
            FileManager.DeleteFile(entity.image!, FileManager.FileStorePAth.img, Environment);
            entity.image = FileManager.storeAs(file, FileManager.FileStorePAth.img, Environment);
            await base.UpdateAsync(entity, id);
        }
    }
}