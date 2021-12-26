using Microsoft.EntityFrameworkCore.ChangeTracking;
using webApp.Manager;
using webApp.Models;

namespace webApp.Data.Repository.Brands;

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
    public BrandRepository(ShopContext context) : base(context)
    {
    }

    public async ValueTask<EntityEntry<Brand>> AddAsync(Brand entity, IFormFile file,
        IWebHostEnvironment Environment)
    {
        entity.icon = FileManager.storeAs(file, FileManager.FileStorePAth.img, Environment);
        return await base.AddAsync(entity);
    }

    public async void Remove(Brand entity, IWebHostEnvironment Environment)
    {
        FileManager.DeleteFile(entity.icon,FileManager.FileStorePAth.img,Environment);
        base.Remove(entity);
    }
}