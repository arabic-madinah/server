namespace MyArabic.WebApi.Features.Chapters.CreateChapter;

public class CreateChapterRequest
{
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public int? Order { get; set; }
}
