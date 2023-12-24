using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.users.Models;

public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  [JsonIgnore]
  public byte[] PasswordHash { get; set; } = [];
  [JsonIgnore]
  public byte[] PasswordSalt { get; set; } = [];
  // public string Email { get; set; } = string.Empty;
  // public string Phone { get; set; } = string.Empty;
  // public required string Address { get; set; }
}
