namespace MadinahArabic.Persistence.ViewModels;

public class ChapterWithLessonTitles
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public List<LessonTitle> Lessons { get; set; }
}