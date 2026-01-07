using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public DateTime DOB { get; set; }
    public int GenderId { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string HashedPassword { get; set; }
    
    //Navigation 
    public Gender Gender { get; set; }
}