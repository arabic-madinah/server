namespace MyArabic.WebApi.Features.Lessons.UpdateLesson;

public class UpdateLessonResponse
{
    public required Guid Id { get; set; }
    public required Guid ChapterId { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required int Order { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}