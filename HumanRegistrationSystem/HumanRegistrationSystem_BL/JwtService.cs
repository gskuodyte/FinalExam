using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HumanRegistrationSystem_Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HumanRegistrationSystem_BL;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetJwtToken(UserAccount userAccount)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userAccount.UserName),
            new(ClaimTypes.Role, userAccount.Role),
            new("sub", userAccount.Id.ToString())
        };

        var secret = _configuration.GetSection("Jwt:Key").Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            _configuration.GetSection("Jwt:Issuer").Value,
            _configuration.GetSection("Jwt:Audience").Value,
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}