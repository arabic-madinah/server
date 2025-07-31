using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.DataAccess;
using MyArabic.WebApi.Exceptions;
using MyArabic.WebApi.Models;

namespace MyArabic.WebApi.Features.Chapters.UpdateChapter;

public class UpdateChapterHandler(AppDbContext context)
{
    private readonly ReOrderEntityRepository _repository = new (context);

    public async Task<UpdateChapterResponse> UpdateChapterAsync(UpdateChapterRequest request,
        CancellationToken cancellationToken)
    {
        var chapter = await
            context
            .Chapters
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (chapter is null)
            throw new NotFoundException($"Chapter by id {request.Id} not found");

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var date = DateTime.UtcNow;
            chapter.Title = request.Title ?? chapter.Title;
            chapter.Slug = request.Slug ?? chapter.Slug;
            chapter.Content = request.Content ?? chapter.Content;
            if (request.Order.HasValue)
                chapter.Order = await _repository.ReOrderEntityAsync<Chapter>(request.Order, cancellationToken);
            chapter.UpdatedAt = date;
            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return new UpdateChapterResponse
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Slug = chapter.Slug,
                Order = chapter.Order,
                Content = chapter.Content,
                CreatedAt = chapter.CreatedAt,
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
