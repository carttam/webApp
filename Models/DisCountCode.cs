using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class DisCountCode
    {
        public int DisCountCodeID { get; set; }
        [DataType(DataType.Text)]
        public string? code { get; set; }
        [DataType(DataType.Date)]
        public DateTime? gen_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime? exp_date { get; set; }
        [Range(0,100)]
        public int? discount { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
