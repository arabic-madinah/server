using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;

namespace MyArabic.WebApi.Features.Chapters.GetChapters;

public class GetChaptersHandler(AppDbContext context)
{
    public async Task<GetChaptersResponse> GetChaptersAsync(
        GetChapterRequest request,
        CancellationToken cancellationToken)
    {
        var chapters = await context
            .Chapters
            .Select(x => new GetChaptersResponse.ChapterDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                Order = x.Order,
                Lessons = x.Lessons
                    .OrderBy(l => l.Order)
                    .Select(l => new GetChaptersResponse.LessonDto
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Slug = l.Slug,
                        Order = l.Order
                    })
                    .ToList(),
            })
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);

        return new GetChaptersResponse
        {
            Chapters = chapters
        };
    }
}