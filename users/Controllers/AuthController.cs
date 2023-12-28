using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Api.Context;
using Api.users.DTOs;
using Api.users.Interfaces;
using Api.users.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.users.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(DataContext db, ITokenService tokenService, IMapper _mapper) : ControllerBase
{
  private readonly DataContext db = db;
  private readonly ITokenService tokenService = tokenService;
  private readonly IMapper mapper = _mapper;


  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(RegisterDTO input)

  {
    string username = input.Username.ToLower();
    var existingUser = await db.Users.FirstOrDefaultAsync((user) => user.Username.ToLower() == username);

    if (existingUser is not null) return BadRequest("User with username already exist");
    using var hmac = new HMACSHA512();

    var user = new User
    {
      Username = username.ToLower(),
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
      PasswordSalt = hmac.Key
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return user;
  }
  [HttpPost("login")]
  public async Task<ActionResult<LoginResponse>> Login(LoginDTO input)
  {
    string UserName = input.Username.ToLower();
    var user = await db.Users.FirstOrDefaultAsync((user) => user.Username == UserName);

    if (user is null) return Unauthorized("User with username not found");

    using var hmac = new HMACSHA512(user.PasswordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password));
    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
    }

    string token = tokenService.CreateToken(user);
    var response = new LoginResponse(Token: token, Id: user.Id, Username: user.Username);


    return response;

  }
  [Authorize]
  [HttpGet("me")]
  public async Task<ActionResult<MemberDTO>> GetMe()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

    if (id is null) return Unauthorized("Id not found");
    var user = await db.Users.FindAsync(int.Parse(id));
    if (user is null) return NotFound("User not found");
    return mapper.Map<MemberDTO>(user);
    // var user = await db.Users.FirstOrDefaultAsync((u)=>identity.Identity.Name)
  }
}
