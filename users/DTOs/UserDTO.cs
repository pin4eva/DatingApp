using System.ComponentModel.DataAnnotations;

namespace Api.users.DTOs;

public record CreateUserDTO(
[Required] string UserName,
[Required] string Password
);


public record UserDTO(
string Username,
string Token
);

