using Api.Context;
using Api.users.DTOs;
using Api.users.Models;
using Api.users.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.users.Controllers;
[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IUserRepository userRepository, IMapper _mapper) : ControllerBase
{
  public readonly IUserRepository repository = userRepository;
  public readonly IMapper mapper = _mapper;

  [AllowAnonymous]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
  {
    var users = await repository.GetUsers();
    var mappedUsers = mapper.Map<IEnumerable<MemberDTO>>(users);
    return Ok(mappedUsers);
  }

  [HttpGet("{id}")]

  public async Task<ActionResult<MemberDTO>> GetUser(int id)
  {
    var user = await repository.GetUserById(id);
    if (user is null) return NotFound("Invalid user ID");
    var mappedUser = mapper.Map<MemberDTO>(user);
    return Ok(mappedUser);
  }

  [HttpGet("members/{id}")]

  public async Task<ActionResult<MemberDTO>> GetMember(int id)
  {
    var user = await repository.GetMemberById(id);
    if (user is null) return NotFound("Invalid user ID");
    var mappedUser = mapper.Map<MemberDTO>(user);
    return Ok(mappedUser);
  }

  [HttpGet("username/{username}")]

  public async Task<ActionResult<User>> GetUserByUsername(string username)
  {
    var user = await repository.GetUserByUsername(username);
    if (user is null) return NotFound("Invalid user username");
    var mappedUser = mapper.Map<MemberDTO>(user);
    return Ok(mappedUser);
  }

}

