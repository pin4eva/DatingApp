using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.users.DTOs;

public record CreateUserDTO(
[Required] string UserName,
[Required] string Password
);
