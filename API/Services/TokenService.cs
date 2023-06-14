using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration configuration)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
    }
    
    public string GenerateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}