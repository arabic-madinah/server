namespace MadinahArabic.Models;

public class QuizBlock : Block
{
    public string Question { get; set; }
    public ICollection<QuizOption> Options { get; set; }
}

public class QuizOption
{
    public int Id { get; set; }
    public int QuizBlockId { get; set; }
    public QuizBlock QuizBlock { get; set; }
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; }
}