namespace MadinahArabic.Models;

public class Chapter
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public List<Lesson> Lessons { get; set; }
}