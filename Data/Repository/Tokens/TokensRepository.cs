using webApp.Models;

namespace webApp.Data.Repository.Tokens;

public class TokensRepository : GenericRepository<Token>, ITokenRepository
{
    public TokensRepository(ShopContext context) : base(context)
    {
    }
}