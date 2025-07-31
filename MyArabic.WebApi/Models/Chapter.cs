namespace MyArabic.WebApi.Models;

public class Chapter : ISortable
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public int Order { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<Lesson> Lessons { get; set; } = new();
}
