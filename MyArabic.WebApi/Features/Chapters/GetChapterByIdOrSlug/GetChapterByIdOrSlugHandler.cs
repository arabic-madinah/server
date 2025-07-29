using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Exceptions;

namespace MyArabic.WebApi.Features.Chapters.GetChapterByIdOrSlug;

public class GetChapterByIdOrSlugHandler(AppDbContext context)
{
    public async Task<GetChapterByIdOrSlugResponse> GetChapterByIdOrSlugAsync(GetChapterByIdOrSlugRequest request)
    {
        var chapter = await context.Chapters
            .Where(c => c.Slug == request.Slug || c.Id.ToString() == request.Slug)
            .Select(c => new GetChapterByIdOrSlugResponse
            {
                Id = c.Id,
                Title = c.Title,
                Slug = c.Slug,
                Order = c.Order,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            })
            .FirstOrDefaultAsync();

        if (chapter is null)
        {
            throw new NotFoundException($"Chapter with slug or Id' {request.Slug}' not found.");
        }

        return chapter;
    }
}