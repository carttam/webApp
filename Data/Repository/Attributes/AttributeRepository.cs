using Microsoft.EntityFrameworkCore.ChangeTracking;
using Attribute = webApp.Models.Attribute;

namespace webApp.Data.Repository.Attributes;

public class AttributeRepository : GenericRepository<Attribute> , IAttributeRepository
{
    public AttributeRepository(ShopContext context) : base(context)
    {
    }
    public override async ValueTask<EntityEntry<Attribute>> AddAsync(Attribute entity)
    {
        if (entity.datas!.Split(",").Length == entity.titles!.Split(",").Length)
            return await base.AddAsync(entity);
        throw new Exception("Invalid Value for Attribute ...");
    } 
}