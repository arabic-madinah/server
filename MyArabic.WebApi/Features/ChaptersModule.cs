using AspNetCore.SecurityKey;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Features.Chapters.CreateChapter;
using MyArabic.WebApi.Features.Chapters.GetChapterByIdOrSlug;
using MyArabic.WebApi.Features.Chapters.GetChapters;
using MyArabic.WebApi.Features.Chapters.UpdateChapter;

namespace MyArabic.WebApi.Features;

public static class ChaptersModule
{
    public const string GetChapterByIdName = "GetChapterById";
    public const string GetChaptersName = "GetChapters";
    public const string CreateChapterName = "CreateChapter";
    public const string PatchChapterName = "PatchChapter";

    public static IEndpointRouteBuilder MapChaptersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/api/chapters/{id}", async (AppDbContext context, string id) =>
            {
                var handler = new GetChapterByIdOrSlugHandler(context);
                var request = new GetChapterByIdOrSlugRequest { Slug = id };
                var chapter = await handler.GetChapterByIdOrSlugAsync(request);
                return Results.Ok(chapter);
            })
            .WithName(GetChapterByIdName);

        endpoints
            .MapGet("/api/chapters", async (AppDbContext db, CancellationToken cancellationToken) =>
            {
                var handler = new GetChaptersHandler(db);
                var chapters = await handler.GetChaptersAsync(new GetChapterRequest(), cancellationToken);
                return Results.Ok(chapters);
            })
            .WithName(GetChaptersName);

        endpoints
            .MapPost("/api/chapters",
                async (AppDbContext db, CreateChapterRequest request, CancellationToken cancellationToken) =>
                {
                    var handler = new CreateChapterHandler(db);
                    var chapter = await handler.CreateChapterAsync(request, cancellationToken);
                    return Results.CreatedAtRoute(GetChapterByIdName, new { Id = chapter.Id }, chapter);
                })
            .WithName(CreateChapterName)
            .RequireSecurityKey();

        endpoints
            .MapPatch("/api/chapters/{id:guid}",
                async (AppDbContext db, Guid id, UpdateChapterRequest request, CancellationToken cancellationToken) =>
                {
                    request.Id = id;
                    var handler = new UpdateChapterHandler(db);
                    var chapter = await handler.UpdateChapterAsync(request, cancellationToken);
                    return Results.Ok(chapter);
                })
            .WithName(PatchChapterName)
            .RequireSecurityKey();

        return endpoints;
    }
}