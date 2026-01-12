namespace AuthMicroservice.Services;

public interface IJWTTokenGenerator
{
    string GenerateToken(string userName);

}