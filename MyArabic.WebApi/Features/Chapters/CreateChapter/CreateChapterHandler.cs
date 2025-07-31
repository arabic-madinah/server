using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Models;

namespace MyArabic.WebApi.Features.Chapters.CreateChapter;

public class CreateChapterHandler(AppDbContext context)
{
    private readonly ReOrderEntityRepository _repository = new (context);

    public async Task<CreateChapterResponse> CreateChapterAsync(
        CreateChapterRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Title.Length is < 2 or > 100)
        {
            throw new ValidationException("Title must be between 2 and 100 characters.");
        }
        if (request.Slug.Length is < 3 or > 50)
        {
            throw new ValidationException("Slug must be between 3 and 50 characters.");
        }

        var existing = await context
            .Chapters
            .Where(x => x.Slug == request.Slug)
            .FirstOrDefaultAsync(cancellationToken);
        if (existing is not null)
        {
            throw new ValidationException("Chapter with the same slug already exists.");
        }

        var chapter = new Chapter
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Slug = request.Slug,
            Content = request.Content,
            Order = await _repository.ReOrderEntityAsync<Chapter>(request.Order, cancellationToken),
            CreatedAt = DateTime.UtcNow,
        };

        var entry = context.Chapters.Add(chapter);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateChapterResponse
        {
            Id = entry.Entity.Id,
            Title = entry.Entity.Title,
            Slug = entry.Entity.Slug,
            Order = entry.Entity.Order,
            CreatedAt = entry.Entity.CreatedAt
        };
    }
}
