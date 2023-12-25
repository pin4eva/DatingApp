using System.ComponentModel.DataAnnotations.Schema;
using Api.users.Models;

namespace Api.photos.Models;

[Table("Photos")]
public class Photo
{
  public int Id { get; set; }
  public string Url { get; set; } = string.Empty;
  public string PublicId { get; set; } = string.Empty;

  public bool IsMain { get; set; }

  public int UserId { get; set; }
  public User User { get; set; }
}
