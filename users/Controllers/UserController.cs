using Api.Context;
using Api.users.DTOs;
using Api.users.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.users.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
  public readonly DataContext db;

  public UserController(DataContext _db)
  {
    db = _db;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    var users = await db.Users.ToListAsync();
    return users;
  }

  [HttpPost]
  public async Task<ActionResult<User>> CreateUser(CreateUserDTO input)
  {
    try
    {
      var user = await db.Users.SingleOrDefaultAsync(user => user.UserName.ToLower() == input.UserName);
      if (user is not null) return BadRequest("user already exist");

      // User newUser = new()
      // {
      //   Name = input.Name,
      //   Email = input.Email,
      //   Phone = input.Phone,
      //   Address = input.Address
      // };

      // await db.Users.AddAsync(newUser);
      // await db.SaveChangesAsync();
      return Ok(user);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }

  }
}
