using AspNetCore.SecurityKey;
using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddDotNetEnv();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddSecurityKey();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
var app = builder.Build();
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapChaptersEndpoints();
app.MapLessonsEndpoints();


app.Run();
