using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class Payment
    {
        public int PaymentID { get; set; } 
        public int? UserID { get; set; }
        [DataType(DataType.Currency)]
        public double? total_price { get; set; }
        public string? paymentCode { get; set; }
        public int? discountCodeID { get; set; }
        public virtual ICollection<Sell>? Sells { get; set; }
        public virtual DisCountCode? DisCountCode { get; set; }
        public virtual User? User { get; set; }
    }
}
