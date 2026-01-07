namespace AuthMicroservice.Dtos;

public class LoginResponseDetails
{
    public string Message { get; set; }
    public LoginResponseDto ResponseDto { get; set; }
    public bool IsSuccess { get; set; }
}