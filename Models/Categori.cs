using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class Categori
    {
        public int CategoriID { get; private set; }
        [DataType(DataType.Text)] public string? title { get; set; }
        public virtual ICollection<SubCategori>? subCategoris { get; set; }
    }
}