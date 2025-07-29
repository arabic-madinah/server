namespace MyArabic.WebApi.Features.Chapters.UpdateChapter;

public class UpdateChapterRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public int? Order { get; set; }
}