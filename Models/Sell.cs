namespace webApp.Models
{
    public class Sell
    {
        public int SellID { get; private set; }
        public int? ObjectID { get; set; }
        public int? UserID { get; set; }
        public int? PaymentID { get; set; }
        public virtual Object? Object { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual User? User { get; set; }
    }
}