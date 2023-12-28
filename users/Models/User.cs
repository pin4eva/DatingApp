using System.Text.Json.Serialization;
using Api.photos.Models;

namespace Api.users.Models;

public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public string KnownAs { get; set; } = string.Empty;
  public string Introduction { get; set; } = string.Empty;
  public string LookingFor { get; set; } = string.Empty;
  public string Interest { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public string Gender { get; set; } = string.Empty;
  public List<Photo> Photos { get; set; } = [];
  public DateOnly DateOfBirth { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public DateTime LastActive { get; set; } = DateTime.UtcNow;
  [JsonIgnore]
  public byte[] PasswordHash { get; set; } = [];
  [JsonIgnore]
  public byte[] PasswordSalt { get; set; } = [];

  // public int GetAge()

  // {
  //   return DateOfBirth.CalculateAge();
  // }
}
