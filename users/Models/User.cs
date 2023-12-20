using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.users.Models;

public class User
{
  public int Id { get; set; }
  public required string UserName { get; set; }
  public byte[] PasswordHash { get; set; } = [];
  public byte[] PasswordSalt { get; set; } = [];
  // public string Email { get; set; } = string.Empty;
  // public string Phone { get; set; } = string.Empty;
  // public required string Address { get; set; }
}
