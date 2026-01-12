namespace AuthMicroservice.Dtos;

public class RegisterResponseDetails
{
    public string Message { get; set; }
    public LoginResponseDto ResponseDto { get; set; }
    public bool IsSuccess { get; set; }
    public List<String> Errors { get; set; }
}
    
public class FluentValidationResponseDetails
{
        
    public string Message { get; set; }
    public List<String> Errors { get; set; }
}
