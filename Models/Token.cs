using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class Token
    {
        public int TokenID { get; set; }
        [DataType(DataType.Text)]
        public string? token {  get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? gen_dateTime { get; set; }
        [DataType(DataType.DateTime)]
        public virtual DateTime? exp_dateTime { get; set; }
        public int? UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
