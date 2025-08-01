using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Features.Chapters.CreateChapter;
using MyArabic.WebApi.Features.Chapters.GetChapterByIdOrSlug;
using MyArabic.WebApi.Features.Chapters.GetChapters;
using MyArabic.WebApi.Features.Chapters.UpdateChapter;
using MyArabic.WebApi.Features.Lessons.CreateLesson;
using MyArabic.WebApi.Features.Lessons.GetLessonByIdOrSlug;
using MyArabic.WebApi.Features.Lessons.UpdateLesson;
using MyArabic.WebApi.Features.Reorder;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddDotNetEnv();
builder.Services.AddOpenApi();
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

app.MapGet("/api/chapters/{id}", async (AppDbContext context, string id) =>
{
    var handler = new GetChapterByIdOrSlugHandler(context);
    var request = new GetChapterByIdOrSlugRequest { Slug = id };
    var chapter = await handler.GetChapterByIdOrSlugAsync(request);
    return Results.Ok(chapter);
}).WithName("GetChapterById");

app.MapGet("/api/chapters", async (AppDbContext db, CancellationToken cancellationToken) =>
{
    var handler = new GetChaptersHandler(db);
    var chapters = await handler.GetChaptersAsync(new GetChapterRequest(), cancellationToken);
    return Results.Ok(chapters);
});

app.MapPost("/api/chapters", async (AppDbContext db, CreateChapterRequest request, CancellationToken cancellationToken) =>
{
    var handler = new CreateChapterHandler(db);
    var chapter = await handler.CreateChapterAsync(request, cancellationToken);
    return Results.CreatedAtRoute("GetChapterById", new { Id = chapter.Id }, chapter);
});

app.MapPatch("/api/chapters/{id:guid}",
    async (AppDbContext db, Guid id, UpdateChapterRequest request, CancellationToken cancellationToken) =>
    {
        request.Id = id;
        var handler = new UpdateChapterHandler(db);
        var chapter = await handler.UpdateChapterAsync(request, cancellationToken);
        return Results.Ok(chapter);
    });

app.MapGet("/api/lessons/{id}", async (AppDbContext context, string id, CancellationToken cancellationToken) =>
{
    var handler = new GetLessonByIdOrSlugHandler(context);
    var request = new GetLessonByIdOrSlugRequest { Slug = id };
    var lesson = await handler.GetLessonByIdOrSlugAsync(request, cancellationToken);
    return Results.Ok(lesson);
}).WithName("GetLessonById");

app.MapPost("/api/lessons", async (AppDbContext db, CreateLessonRequest request, CancellationToken cancellationToken) =>
{
    var handler = new CreateLessonHandler(db);
    var lesson = await handler.CreateLessonAsync(request, cancellationToken);
    return Results.CreatedAtRoute("GetLessonById", new { Id = lesson.Id }, lesson);
});

app.MapPatch("/api/lessons/{id:guid}",
    async (AppDbContext db, Guid id, UpdateLessonRequest request, CancellationToken cancellationToken) =>
    {
        request.Id = id;
        var handler = new UpdateLessonHandler(db);
        var lesson = await handler.UpdateLessonAsync(request, cancellationToken);
        return Results.Ok(lesson);
    });

app.MapPost("/api/reorder", async (AppDbContext db, ReorderChaptersWithLessonsRequest request, CancellationToken cancellationToken) =>
{
    var handler = new ReorderChaptersWithLessonsHandler(db);
    await handler.ReorderChaptersWithLessonsAsync(request, cancellationToken);
    return Results.NoContent();
});

app.Run();
