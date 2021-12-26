using webApp.Models;

namespace webApp.Data.Repository.SubCategoris;

public class SubCategoriRepository : GenericRepository<SubCategori>, ISubCategoriRepository
{
    public SubCategoriRepository(ShopContext context) : base(context)
    {
    }
}