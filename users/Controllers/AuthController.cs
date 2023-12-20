using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Context;
using Api.users.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.users.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(DataContext db) : ControllerBase
{
  private readonly DataContext db = db;

  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(string username, string password)

  {
    var existingUser = await db.Users.FirstOrDefaultAsync((user) => user.UserName.ToLower() == username);

    if (existingUser is not null) return BadRequest("User with username already exist");
    using var hmac = new HMACSHA512();

    var user = new User
    {
      UserName = username.ToLower(),
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
      PasswordSalt = hmac.Key
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return user;
  }
}
