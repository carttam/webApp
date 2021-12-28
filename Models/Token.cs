using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class Token
    {
        public int TokenID { get; private set; }
        [DataType(DataType.Text)] public string? token { get; set; }
        [DataType(DataType.DateTime)] public DateTime? gen_dateTime { get;  set; }
        [DataType(DataType.DateTime)] public virtual DateTime? exp_dateTime { get;  set; }
        public int? UserID { get; set; }
        public virtual User? User { get; set; }

        public void setExpGenDateTime(DateTime gen_dateTime, DateTime exp_dateTime)
        {
            this.gen_dateTime = gen_dateTime;
            this.exp_dateTime = exp_dateTime;
        }
    }
}