using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.Dtos;

public class LoginRequest
{
    [Required(ErrorMessage = "EmailAddress cannot be empty")]
    public string EmailAddress { get; set; }
    
    [Required(ErrorMessage = "Enter your password")]
    public string Password { get; set; }
}