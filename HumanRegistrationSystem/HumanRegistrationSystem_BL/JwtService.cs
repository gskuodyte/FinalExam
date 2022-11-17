using HumanRegistrationSystem_Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HumanRegistrationSystem_BL
{
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
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role),
                new Claim("sub", userAccount.Id.ToString())
            };

            var secret = _configuration.GetSection("Jwt:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
