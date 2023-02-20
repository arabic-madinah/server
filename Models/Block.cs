namespace MadinahArabic.Models;

public abstract class Block
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Order { get; set; }
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; }
}