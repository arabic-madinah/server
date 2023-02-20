namespace MadinahArabic.Models;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public int ChapterId { get; set; }
    public Chapter Chapter { get; set; }
    public List<Block> Blocks { get; set; }
}