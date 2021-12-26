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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        public IObjectRepository Object { get; private set; }
        public IAttributeRepository Attribute { get; }
        public IBrandRepository Brand { get; }
        public ICategoriRepository Categori { get; }
        public IDisCountCodeRepository DisCountCode { get; }
        public IPaymentRepository Payment { get; }
        public ISellRepository Sell { get; }
        public ISubCategoriRepository SubCategori { get; }
        public ITokenRepository Token { get; }
        public IUserRepository User { get; }

        public UnitOfWork(ShopContext context)
        {
            _context = context;
            Object = new ObjectRepository(_context);
            Attribute = new AttributeRepository(_context);
            Brand = new BrandRepository(_context);
            Categori = new CategoriRepository(_context);
            DisCountCode = new DisCountCodeRepository(_context);
            Payment = new PaymentRepository(_context);
            Sell = new SellRepository(_context);
            SubCategori = new SubCategoriRepository(_context);
            Token = new TokensRepository(_context);
            User = new UserRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
