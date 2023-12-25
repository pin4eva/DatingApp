using Api.users.DTOs;
using Api.users.Models;

namespace Api.users.Repository;

public interface IUserRepository
{
  void Update(User user);
  Task<bool> SaveAllAsync();
  Task<IEnumerable<User>> GetUsers();
  Task<User?> GetUserById(int id);
  Task<User?> GetUserByUsername(string username);
  Task<IEnumerable<MemberDTO>> GetMembers();
  Task<MemberDTO?> GetMemberById(int id);
  Task<MemberDTO?> GetMemberByUsername(string username);
}
