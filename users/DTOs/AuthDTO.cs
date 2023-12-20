using System.ComponentModel.DataAnnotations;

namespace Api.users.DTOs;

public record RegisterDTO(
[Required] string UserName,
[Required] string Password
);


public record LoginDTO(
[Required] string UserName,
[Required] string Password
);

public record LoginResponse(
string UserName,
string Token,
int Id
);

