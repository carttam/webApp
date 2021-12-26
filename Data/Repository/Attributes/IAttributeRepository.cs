using Microsoft.EntityFrameworkCore.ChangeTracking;
using Attribute = webApp.Models.Attribute;

namespace webApp.Data.Repository.Attributes;

public interface IAttributeRepository : IGenericRepository<Attribute>
{
}