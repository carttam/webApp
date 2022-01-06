using Microsoft.EntityFrameworkCore;
using webApp.Models;

namespace webApp.Data.Repository.Categoris;

public class CategoriRepository : GenericRepository<Categori>, ICategoriRepository
{
    public CategoriRepository(ShopContext context) : base(context)
    {
    }

    public override async Task<List<Categori>> AllAsync()
    {
        return await this._context.Categoris.Include(c=>c.subCategoris).ThenInclude(sc=>sc.Objects).ToListAsync();
    }
}