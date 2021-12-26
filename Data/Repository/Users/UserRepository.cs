using webApp.Models;

namespace webApp.Data.Repository.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ShopContext context) : base(context)
    {
    }
}