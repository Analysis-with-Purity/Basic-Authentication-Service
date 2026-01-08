using AuthMicroservice.Dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = AuthMicroservice.Dtos.LoginRequest;
using RegisterRequest = AuthMicroservice.Dtos.RegisterRequest;

namespace API.Interfaces;

public interface IAuthService
{
    RegisterResponseDetails RegisterUser(RegisterRequest user);
    LoginResponseDetails Login(LoginRequest loginRequest/*, [FromServices] JwtTokenGenerator tokenGenerator*/);
}