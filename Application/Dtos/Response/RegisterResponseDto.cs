namespace AuthMicroservice.Dtos;

public class RegisterResponseDto
{
    public int UserId { get; set; }
    public int UserName { get; set; }
    public string EmailAddress { get; set; }
    public int  GenderId { get; set; }
}