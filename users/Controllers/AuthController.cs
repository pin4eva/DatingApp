using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Context;
using Api.users.DTOs;
using Api.users.Interfaces;
using Api.users.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.users.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(DataContext db, ITokenService tokenService) : ControllerBase
{
  private readonly DataContext db = db;
  private readonly ITokenService tokenService = tokenService;


  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(RegisterDTO input)

  {
    string username = input.UserName.ToLower();
    var existingUser = await db.Users.FirstOrDefaultAsync((user) => user.UserName.ToLower() == username);

    if (existingUser is not null) return BadRequest("User with username already exist");
    using var hmac = new HMACSHA512();

    var user = new User
    {
      UserName = username.ToLower(),
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
    string UserName = input.UserName.ToLower();
    var user = await db.Users.FirstOrDefaultAsync((user) => user.UserName == UserName);

    if (user is null) return Unauthorized("User with username not found");

    using var hmac = new HMACSHA512(user.PasswordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password));
    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
    }

    string token = tokenService.CreateToken(user);
    var response = new LoginResponse(Token: token, Id: user.Id, UserName: user.UserName);


    return response;

  }
}
