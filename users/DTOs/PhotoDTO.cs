namespace Api.users.DTOs;

public class PhotoDTO
{
  public int Id { get; set; }
  public string Url { get; set; } = string.Empty;
  public string PublicId { get; set; } = string.Empty;

  public bool IsMain { get; set; }

  // public int UserId { get; set; }
}
