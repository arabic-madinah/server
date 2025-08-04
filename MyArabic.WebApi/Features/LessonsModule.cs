using AspNetCore.SecurityKey;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Features.Lessons.CreateLesson;
using MyArabic.WebApi.Features.Lessons.GetLessonByIdOrSlug;
using MyArabic.WebApi.Features.Lessons.UpdateLesson;
using MyArabic.WebApi.Features.Reorder;

namespace MyArabic.WebApi.Features;

public static class LessonsModule
{
    public const string GetLessonByIdName = "GetLessonById";
    public const string CreateLessonName = "CreateLesson";
    public const string PatchLessonName = "PatchLesson";

    public static IEndpointRouteBuilder MapLessonsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/api/lessons/{id}", async (AppDbContext context, string id, CancellationToken cancellationToken) =>
            {
                var handler = new GetLessonByIdOrSlugHandler(context);
                var request = new GetLessonByIdOrSlugRequest { Slug = id };
                var lesson = await handler.GetLessonByIdOrSlugAsync(request, cancellationToken);
                return Results.Ok(lesson);
            })
            .WithName(GetLessonByIdName);

        endpoints
            .MapPost("/api/lessons",
                async (AppDbContext db, CreateLessonRequest request, CancellationToken cancellationToken) =>
                {
                    var handler = new CreateLessonHandler(db);
                    var lesson = await handler.CreateLessonAsync(request, cancellationToken);
                    return Results.CreatedAtRoute(GetLessonByIdName, new { Id = lesson.Id }, lesson);
                })
            .WithName(CreateLessonName)
            .RequireSecurityKey();

        endpoints
            .MapPatch("/api/lessons/{id:guid}", async (AppDbContext db, Guid id, UpdateLessonRequest request,
                CancellationToken cancellationToken) =>
            {
                request.Id = id;
                var handler = new UpdateLessonHandler(db);
                var lesson = await handler.UpdateLessonAsync(request, cancellationToken);
                return Results.Ok(lesson);
            })
            .WithName(PatchLessonName)
            .RequireSecurityKey();

        endpoints
            .MapPost("/api/reorder",
                async (AppDbContext db, ReorderChaptersWithLessonsRequest request,
                    CancellationToken cancellationToken) =>
                {
                    var handler = new ReorderChaptersWithLessonsHandler(db);
                    await handler.ReorderChaptersWithLessonsAsync(request, cancellationToken);
                    return Results.NoContent();
                })
            .RequireSecurityKey();

        return endpoints;
    }
}