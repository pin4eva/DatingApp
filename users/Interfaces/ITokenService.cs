using Api.users.Models;

namespace Api.users.Interfaces;

public interface ITokenService
{
  string CreateToken(User user);
}
