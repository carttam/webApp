using webApp.Models;

namespace webApp.Data.Repository.Sells;

public class SellRepository : GenericRepository<Sell>, ISellRepository
{
    public SellRepository(ShopContext context) : base(context)
    {
    }
}