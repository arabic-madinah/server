using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Exceptions;

namespace MyArabic.WebApi.Features.Lessons.GetLessonByIdOrSlug;

public class GetLessonByIdOrSlugHandler(AppDbContext context)
{
    public async Task<GetLessonByIdOrSlugResponse> GetLessonByIdOrSlugAsync(
        GetLessonByIdOrSlugRequest request,
        CancellationToken cancellationToken)
    {
        var lesson = await context
            .Lessons
            .Where(x => x.Slug == request.Slug)
            .Select(x => new GetLessonByIdOrSlugResponse
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                Order = x.Order,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Chapter = new GetLessonByIdOrSlugResponse.ChapterDto
                {
                    Id = x.ChapterId,
                    Title = x.Chapter.Title,
                    Slug = x.Chapter.Slug,
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (lesson is null)
            throw new NotFoundException($"Lesson with slug '{request.Slug}' not found.");
        
        return lesson;
    }
}