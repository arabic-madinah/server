namespace MyArabic.WebApi.Features.Lessons.CreateLesson;

public class CreateLessonResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required int Order { get; set; }
}