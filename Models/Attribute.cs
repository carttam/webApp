using System.ComponentModel.DataAnnotations;
using Object = webApp.Models.Object;

namespace webApp.Models
{
    public class Attribute
    {
        public int AttributeID { get; private set; }
        [DataType(DataType.MultilineText)] public string? titles { get; set; }
        [DataType(DataType.MultilineText)] public string? datas { get; set; }
        public virtual Object? Object { get; private set; }
    }
}