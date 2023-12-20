using Api.Context;
using Api.users.DTOs;
using Api.users.Models;
using Microsoft.AspNetCore.Authorization;
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

  [Authorize]
  [HttpGet("{id}")]

  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);
    if (user is null) return NotFound("Invalid user ID");
    return user;
  }

}

