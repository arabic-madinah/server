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