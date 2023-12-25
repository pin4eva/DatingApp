using System.Text;
using Api.Context;
using Api.users.Interfaces;
using Api.users.Repository;
using Api.users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Api.Extensions;

public static class ApplicationServiceExtension
{
  public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
    services.AddCors();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      var TokenKey = configuration["TokenKey"];
      if (TokenKey is null)
      {
        Console.WriteLine("please add a TokenKey");
        return;
      }
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
      };
    }
    );
    // Database setting;
    services.AddDbContext<DataContext>(option =>
    {
      option.UseNpgsql(configuration.GetConnectionString("DATABASE_URL"));
    }
    );

    return services;
  }
}
