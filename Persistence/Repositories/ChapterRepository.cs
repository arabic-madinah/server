using MadinahArabic.Models;
using MadinahArabic.Persistence.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MadinahArabic.Persistence.Repositories;

public class ChapterRepository
{
    private readonly ApplicationDbContext _context;

    public ChapterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChapterWithLessonTitles>> GetAllChaptersWithLessonTitles()
    {
        return await _context
            .Chapters
            .Include(c => c.Lessons.OrderBy(l => l.Order))
            .OrderBy(c => c.Order)
            .Select(c => new ChapterWithLessonTitles()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Order = c.Order,
                Lessons = c.Lessons.Select(l => new LessonTitle()
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    Order = l.Order
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task CreateChapterAsync(Chapter chapter)
    {
        await _context.Chapters.AddAsync(chapter);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteChapterAsync(int id)
    {
        var chapter = await _context.Chapters.FindAsync(id);
        if (chapter == null) return;

        _context.Chapters.Remove(chapter);
        await _context.SaveChangesAsync();
    }
    
    

    public async Task ReorderChapterAsync(int chapterId, int newOrder)
    {
        var chapter = await _context.Chapters.FindAsync(chapterId);
        if (chapter == null)
        {
            throw new ArgumentException($"Chapter with id {chapterId} does not exist");
        }
        
        // Get all the chapters in the course
        var chapters = await _context.Chapters
            .OrderBy(c => c.Order)
            .ToListAsync();

        // Get the index of the chapter to be moved
        var oldIndex = chapters.IndexOf(chapter);

        // Remove the chapter from the list
        chapters.RemoveAt(oldIndex);

        // Insert the chapter at the new position
        chapters.Insert(newOrder - 1, chapter);

        // Update the order of all the chapters
        for (var i = 0; i < chapters.Count; i++)
        {
            chapters[i].Order = i + 1;
        }

        await _context.SaveChangesAsync();
    }
}