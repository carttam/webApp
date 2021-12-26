using webApp.Models;

namespace webApp.Data.Repository.Categoris;

public class CategoriRepository : GenericRepository<Categori>, ICategoriRepository
{
    public CategoriRepository(ShopContext context) : base(context)
    {
    }
}