
using Api.Context;
using Api.users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuggyController(DataContext _db) : ControllerBase
{
  private readonly DataContext db = _db;

  [HttpGet("auth")]
  public ActionResult<string> GetSecret()
  {
    return "Secret text";
  }

  [HttpGet("not-found")]
  public ActionResult<User> GetFound()
  {
    var thing = db.Users.Find(-1);
    if (thing is null) return NotFound();
    return thing;
  }

  [HttpGet("server-error")]
  public ActionResult<string> GetServerError()
  {


    var thing = db.Users.Find(-1);
    var thingToReturn = thing.ToString();
    return thingToReturn;


  }

  [HttpGet("bad-request")]
  public ActionResult<string> GetBadRequest()
  {
    return BadRequest("This was not a good request");
  }
}
