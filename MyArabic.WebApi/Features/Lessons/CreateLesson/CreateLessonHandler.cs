using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Models;

namespace MyArabic.WebApi.Features.Lessons.CreateLesson;

public class CreateLessonHandler(AppDbContext context)
{
    private readonly ReOrderEntityRepository _repository = new(context);
    
    public async Task<CreateLessonResponse> CreateLessonAsync(
        CreateLessonRequest request,
        CancellationToken cancellationToken)
    {
        var existing = await context
            .Lessons
            .AnyAsync(l => l.Slug == request.Slug, cancellationToken);
        if (existing)
        {
            throw new ValidationException($"Lesson with slug '{request.Slug}' already exists");
        }
        
        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            ChapterId = request.ChapterId,
            Content = request.Content,
            Title = request.Title,
            Slug = request.Slug,
            Order = await _repository.ReOrderEntityAsync<Lesson>(request.Order, cancellationToken),
            CreatedAt = DateTime.UtcNow,
        };

        context.Lessons.Add(lesson);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateLessonResponse
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Slug = lesson.Slug,
            Order = lesson.Order
        };
    }
}