using DotNetEnv;
using MadinahArabic.MiddleWare;
using MadinahArabic.Persistence;
using MadinahArabic.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Env.Load();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(Env.GetString("DATABASE_URL")));
builder.Services.AddScoped<ChapterRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();