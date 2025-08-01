using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;

namespace MyArabic.WebApi.Features.Reorder;

public class ReorderChaptersWithLessonsHandler(AppDbContext context)
{
    public async Task ReorderChaptersWithLessonsAsync(ReorderChaptersWithLessonsRequest request,
        CancellationToken cancellationToken = default)
    {
        // Load all affected chapters and lessons from DB in one go
        var chapterIds = request.Chapters.Select(c => c.Id).ToList();
        var lessonIds = request.Chapters.SelectMany(c => c.Lessons).Select(l => l.Id).ToList();

        var chapters = await context.Chapters
            .Where(c => chapterIds.Contains(c.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        var lessons = await context.Lessons
            .Where(l => lessonIds.Contains(l.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        // Reorder chapters
        foreach (var chapterRequest in request.Chapters)
        {
            var chapter = chapters.FirstOrDefault(c => c.Id == chapterRequest.Id);

            if (chapter is not null)
            {
                chapter.Order = chapterRequest.Order;
            }

            // Reorder lessons inside the chapter
            foreach (var lessonRequest in chapterRequest.Lessons)
            {
                var lesson = lessons.FirstOrDefault(l => l.Id == lessonRequest.Id);
                if (lesson is null) continue;
                lesson.Order = lessonRequest.Order;
                lesson.ChapterId = chapterRequest.Id;
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
