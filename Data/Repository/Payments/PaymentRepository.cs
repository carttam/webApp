using webApp.Models;

namespace webApp.Data.Repository.Payments;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository 
{
    public PaymentRepository(ShopContext context) : base(context)
    {
    }
}