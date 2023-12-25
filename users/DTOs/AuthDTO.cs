using System.ComponentModel.DataAnnotations;

namespace Api.users.DTOs;

public record RegisterDTO(
[Required] string Username,
[Required, StringLength(6)] string Password
);


public record LoginDTO(
[Required] string Username,
[Required] string Password
);

public record LoginResponse(
string Username,
string Token,
int Id
);

