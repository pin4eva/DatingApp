using System.Text;
using Api.Context;
using Api.users.Interfaces;
using Api.users.Repository;
using Api.users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace Api.Extensions;

public static class ApplicationServiceExtension
{
  public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(option =>
{
  option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "bearer"
  }

  );

  option.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
        new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
          },
        Array.Empty<string>()
    }
  }
);

}

);
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
