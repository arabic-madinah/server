namespace MyArabic.WebApi.Features.Reorder;

public class ReorderChaptersWithLessonsRequest
{
    public class ReorderChapterRequest
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public List<ReorderLessonRequest> Lessons { get; set; } = [];
    }

    public class ReorderLessonRequest
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
    }

    public List<ReorderChapterRequest> Chapters { get; set; } = [];
}
