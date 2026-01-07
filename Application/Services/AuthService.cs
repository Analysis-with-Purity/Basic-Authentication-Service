using AuthMicroservice.Dtos;
using Microsoft.AspNetCore.Authentication;

namespace AuthMicroservice.Services;

public class AuthService : IAuthService
{
    public RegisterResponseDetails RegisterUser(RegisterRequest user)
    {
        throw new NotImplementedException();
    }

    public LoginResponseDetails Login(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }
}