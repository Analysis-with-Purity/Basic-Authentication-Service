using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.Dtos;

public class RegisterRequest
{
    [Required(ErrorMessage = "UserName cannot be empty")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "EmailAddress cannot be empty")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "phoneNo cannot be empty")]
    public string PhoneNo { get; set; }
    
    [Required(ErrorMessage = "Enter a valid Date of Birth")]
    public DateTime DOB { get; set; }
    
    [Required(ErrorMessage = "GenderId cannot be empty")]
    public int GenderId { get; set; }
    
    public string? ProfileImageUrl { get; set; }
    
    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; }
    

}