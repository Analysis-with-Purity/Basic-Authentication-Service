using AuthMicroservice.Dtos;
using AuthMicroservice.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController :Controller
{
    private readonly IAuthService _user;
        
    public AuthController(IAuthService user)
    {
        _user = user;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest user)
    {
        var result = _user.RegisterUser(user);
        return Ok(result);
    }
  
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var result = _user.Login(request);
        return Ok(result);
    }

}