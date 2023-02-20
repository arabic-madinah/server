using MadinahArabic.Models;
using Microsoft.EntityFrameworkCore;

namespace MadinahArabic.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<MarkdownBlock> MarkdownBlocks { get; set; }
    public DbSet<QuizBlock> QuizBlocks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

}