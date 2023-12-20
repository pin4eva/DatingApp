using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")
);

app.UseAuthentication(); // must always come first
app.UseAuthorization();

app.MapControllers();
app.Run();

