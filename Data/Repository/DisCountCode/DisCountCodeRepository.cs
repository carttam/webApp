namespace webApp.Data.Repository.DisCountCode;

public class DisCountCodeRepository : GenericRepository<Models.DisCountCode>, IDisCountCodeRepository
{
    public DisCountCodeRepository(ShopContext context) : base(context)
    {
    }
}