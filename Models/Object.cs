using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class Object
    {
        public int ObjectID { get; private set; }
        [DataType(DataType.Text)] public string? title { get; set; }
        [DataType(DataType.MultilineText)] public string? description { get; set; }
        [DataType(DataType.ImageUrl)] public string? image { get; set; }
        [DataType(DataType.Currency)] public double? price { get; set; }
        [Range(0, 100)] public int? discount { get; set; }
        public int? BrandID { get; set; }
        [Required] public int? SubCategoriID { get; set; }
        public virtual Brand? Brand { get; set; }
        public int? AttributeID { get; set; }
        public virtual Attribute? Attribute { get; set; }
        public virtual SubCategori? SubCategori { get; set; }
        public virtual ICollection<Sell>? Sells { get; private set; }
    }
}