

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.users.Interfaces;
using Api.users.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.users.Services;

public class TokenService : ITokenService
{
  private readonly SymmetricSecurityKey key;
  public TokenService(IConfiguration config)
  {
    key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
  }
  public string CreateToken(User user)
  {
    var claims = new List<Claim>
    {
  new (JwtRegisteredClaimNames.NameId, user.Username),
new ("Id",
     user.Id.ToString())
};

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddDays(7),
      SigningCredentials = creds
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}
