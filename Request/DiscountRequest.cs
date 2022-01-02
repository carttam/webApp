using System.ComponentModel.DataAnnotations;

namespace webApp.Request;

public class DiscountRequest
{
    [MinLength(6)]
    public string d_code { get; set; }
}