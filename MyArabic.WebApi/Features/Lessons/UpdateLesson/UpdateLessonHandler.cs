using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Exceptions;
using MyArabic.WebApi.Models;
using MyArabic.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace MyArabic.WebApi.Features.Lessons.UpdateLesson;

public class UpdateLessonHandler(AppDbContext context)
{
    private readonly ReOrderEntityRepository _repository = new(context);

    public async Task<UpdateLessonResponse> UpdateLessonAsync(
        UpdateLessonRequest request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Slug) && !SlugValidator.Validate(request.Slug))
        {
            throw new ValidationException("Slug is not valid. It must be lowercase, alphanumeric, and can contain dashes.");
        }
        var lesson = await context
            .Lessons
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException($"Lesson with ID {request.Id} not found.");
        }

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var date = DateTime.UtcNow;
            lesson.Title = request.Title ?? lesson.Title;
            lesson.Slug = request.Slug ?? lesson.Slug;
            lesson.ChapterId = request.ChapterId ?? lesson.ChapterId;
            if (request.Order.HasValue)
                lesson.Order = await _repository.ReOrderEntityAsync<Lesson>(request.Order, cancellationToken);
            lesson.Content = request.Content ?? lesson.Content;
            lesson.UpdatedAt = date;
            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new UpdateLessonResponse
            {
                Id = lesson.Id,
                ChapterId = lesson.ChapterId,
                Title = lesson.Title,
                Slug = lesson.Slug,
                Order = lesson.Order,
                CreatedAt = lesson.CreatedAt,
                UpdatedAt = date
            };
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

    }
}
