namespace MyArabic.WebApi.Features.Chapters.CreateChapter;

public class CreateChapterResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required int Order { get; set; }
    public required DateTime CreatedAt { get; set; }
}