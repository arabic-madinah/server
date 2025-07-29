namespace MyArabic.WebApi.Features.Lessons.UpdateLesson;

public class UpdateLessonRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public int? Order { get; set; }
    public string? Content { get; set; }
    public Guid? ChapterId { get; set; }
}