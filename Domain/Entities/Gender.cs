using System.ComponentModel.DataAnnotations;
namespace API.Entities;

public class Gender
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
}