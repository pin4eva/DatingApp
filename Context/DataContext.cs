
using Api.users.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class DataContext : DbContext
{
  protected readonly IConfiguration configuration;

  public DataContext(IConfiguration _configuration, DbContextOptions<DataContext> options) : base(options)
  {
    configuration = _configuration;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  public DbSet<User> Users { get; set; }
}
