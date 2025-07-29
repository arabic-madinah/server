namespace MyArabic.WebApi.Models;

public class Lesson : ISortable
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public int Order { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid ChapterId { get; set; }
    public Chapter Chapter { get; set; } = null!;
}