namespace MyArabic.WebApi.Features.Lessons.GetLessonByIdOrSlug;

public class GetLessonByIdOrSlugResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required int Order { get; set; }
    public required string Content { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public required ChapterDto Chapter { get; set; }
    
    public class ChapterDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
    }
}