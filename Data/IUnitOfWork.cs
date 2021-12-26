using webApp.Data.Repository.Attributes;
using webApp.Data.Repository.Brands;
using webApp.Data.Repository.Categoris;
using webApp.Data.Repository.DisCountCode;
using webApp.Data.Repository.Objects;
using webApp.Data.Repository.Payments;
using webApp.Data.Repository.Sells;
using webApp.Data.Repository.SubCategoris;
using webApp.Data.Repository.Tokens;
using webApp.Data.Repository.Users;

namespace webApp.Data
{
    public interface IUnitOfWork
    {
        IObjectRepository Object { get; }
        IAttributeRepository Attribute { get; }
        IBrandRepository Brand { get; }
        ICategoriRepository Categori { get; }
        IDisCountCodeRepository DisCountCode { get; }
        IPaymentRepository Payment { get; }
        ISellRepository Sell { get; }
        ISubCategoriRepository SubCategori { get; }
        ITokenRepository Token { get; }
        IUserRepository User { get; }

        Task<int> SaveAsync();
    }
}
