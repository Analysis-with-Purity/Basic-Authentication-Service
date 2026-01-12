using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthMicroservice.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.TechnicalImplementations;

public class JWTTokenGenerator: IJWTTokenGenerator
{
    
    private readonly IConfiguration _config;

    public JWTTokenGenerator(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateToken(string userName)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        var keysecret = _config["Appsettings:secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keysecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var expiration = DateTime.UtcNow.AddDays(1);


        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["Appsettings:ValidIssuer"],
            audience: _config["Appsettings:ValidAudience"],
            expires: expiration,
            signingCredentials: credentials,
            claims: claims
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenDescriptor);

    }
}