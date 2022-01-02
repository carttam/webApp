using System.ComponentModel.DataAnnotations;

namespace webApp.Request;

public class LoginRequest
{
    [Required]
    [MaxLength(80)]
    public string username { get; set; }
    [MinLength(8),MaxLength(18)]
    public string password { get; set; }
}