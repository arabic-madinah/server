namespace MyArabic.WebApi.Features.Chapters.GetChapters;

public class GetChaptersResponse
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required int Order { get; set; }
    }
    
    public class ChapterDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required int Order { get; set; }
        
        public required List<LessonDto> Lessons { get; set; }
    }
    
    public required List<ChapterDto> Chapters { get; set; }
}