using AuthMicroservice.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Services;

public interface IAuthService
{
    RegisterResponseDetails RegisterUser(RegisterRequest user);
    LoginResponseDetails Login(LoginRequest loginRequest);
}