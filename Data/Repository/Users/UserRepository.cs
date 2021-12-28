using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using webApp.Models;

namespace webApp.Data.Repository.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ShopContext context) : base(context)
    {
    }

    public override async ValueTask<EntityEntry<User>> AddAsync(User entity)
    {
        var email = await this._context.Users!.Where(u => u.email == entity.email).FirstOrDefaultAsync();
        var username = await this._context.Users!.Where(u => u.username == entity.username).FirstOrDefaultAsync();
        if (email != null || username != null)
            throw new Exception("User With this " + (email != null ? "email" : "") +
                                          (email != null && username != null ? " and " : "") +
                                          (username != null ? "username" : "") + " Already Exist .");
        return await base.AddAsync(entity);
    }

    public override Task<IActionResult> UpdateAsync(User entity, int id)
    {
        if (id != entity.UserID)
        {
            throw new Exception("Bad Request .");
        }
        return base.UpdateAsync(entity, id);
    }

}