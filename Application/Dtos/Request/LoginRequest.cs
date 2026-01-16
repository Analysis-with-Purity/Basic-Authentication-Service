using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.Dtos;

public class LoginRequest
{
    [Required(ErrorMessage = "Email cannot be empty")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Enter your password")]
    public string Password { get; set; }
}