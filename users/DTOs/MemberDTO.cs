namespace Api.users.DTOs;

public class MemberDTO
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public string KnownAs { get; set; } = string.Empty;
  public string PhotoUrl { get; set; } = string.Empty;
  public string Introduction { get; set; } = string.Empty;
  public string LookingFor { get; set; } = string.Empty;
  public string Interest { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public string Gender { get; set; } = string.Empty;
  public int Age { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public DateTime LastActive { get; set; } = DateTime.UtcNow;
  public List<PhotoDTO> Photos { get; set; } = [];

}

public record UpdateMemberDTO(
  int Id,
  string Introduction,
  string LookingFor,
  string Interest,
  string City,
  string Country,
  string Gender
);
