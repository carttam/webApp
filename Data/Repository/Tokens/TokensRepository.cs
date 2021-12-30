using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using webApp.Models;

namespace webApp.Data.Repository.Tokens;

public class TokensRepository : GenericRepository<Token>, ITokenRepository
{
    public TokensRepository(ShopContext context) : base(context)
    {
    }

    public override async Task<Token?> FirstOrDefaultAsync(Expression<Func<Token, bool>> predicate)
    {
        return (await this._context.Tokens!.Include(t=> t.User).FirstOrDefaultAsync(predicate));
    }
}