using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class SubCategori
    {
        public int SubCategoriID { get; set; }
        [DataType(DataType.Text)]
        public string? title {  get; set; }
        public int? CategoriID { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? image { get; set; }
        public virtual Categori? Categori { get; set; }
        public virtual ICollection<Object>? Objects { get; set; }
    }
}
