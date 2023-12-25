
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Api.users.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class Seed
{
  public static async Task SeedUsers(DataContext context)
  {
    if (await context.Users.AnyAsync())
    {
      Console.WriteLine("User exist in db");
      return;
    };

    var userData = await File.ReadAllTextAsync("Context/UserSeedData.json");
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var users = JsonSerializer.Deserialize<List<User>>(userData, options);
    if (users is null)
    {
      Console.WriteLine("User is null");
      return;
    }
    foreach (var user in users)
    {
      using var hmac = new HMACSHA512();
      user.Username = user.Username.ToLower();
      user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
      user.PasswordSalt = hmac.Key;

      context.Users.Add(user);
    }

    await context.SaveChangesAsync();
  }
}
