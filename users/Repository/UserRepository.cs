
using Api.Context;
using Api.users.DTOs;
using Api.users.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.users.Repository;

public class UserRepository(DataContext _db, IMapper _mapper) : IUserRepository
{
  private readonly DataContext db = _db;
  private readonly IMapper mapper = _mapper;

  public async Task<MemberDTO?> GetMemberById(int id)
  {
    var user = await db.Users
.Include(user => user.Photos)
.ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
.FirstOrDefaultAsync((user) => user.Id == id);


    return user;
  }

  public async Task<MemberDTO?> GetMemberByUsername(string username)
  {
    var user = await db.Users
 .Where((user) => user.Username == username)
.ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
.SingleOrDefaultAsync();



    return user;
  }

  public async Task<IEnumerable<MemberDTO>> GetMembers()
  {
    var users = await db.Users.ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
.ToListAsync();
    return users;

  }

  public async Task<User?> GetUserById(int id)
  {
    var user = await db.Users
.Include(user => user.Photos)
.FirstOrDefaultAsync((user) => user.Id == id);


    return user;
  }

  public async Task<User?> GetUserByUsername(string username)
  {
    var user = await db.Users.Include(user => user.Photos).FirstOrDefaultAsync((user) => user.Username == username);

    return user;
  }

  public async Task<IEnumerable<User>> GetUsers()
  {
    return await db.Users.ToListAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await db.SaveChangesAsync() > 0;
  }

  public void Update(User user)
  {
    db.Entry(user).State = EntityState.Modified;
  }
}
