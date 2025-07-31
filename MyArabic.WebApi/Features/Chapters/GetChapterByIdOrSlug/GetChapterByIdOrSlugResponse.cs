namespace MyArabic.WebApi.Features.Chapters.GetChapterByIdOrSlug;

public class GetChapterByIdOrSlugResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public required int Order { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }
}
