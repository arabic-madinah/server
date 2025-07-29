namespace MyArabic.WebApi.Features.Lessons.CreateLesson;

public class CreateLessonRequest
{
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public int? Order { get; set; }
    public required Guid ChapterId { get; set; }
    public required string Content { get; set; }
}

