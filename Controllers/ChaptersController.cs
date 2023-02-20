using MadinahArabic.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MadinahArabic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChaptersController : ControllerBase
{
    private readonly ChapterRepository _chapters;

    public ChaptersController(ChapterRepository chapters)
    {
        _chapters = chapters;
    }

    [HttpGet]
    public async Task<IActionResult> GetChapters()
    {
        var chapters = await _chapters.GetAllChaptersWithLessonTitles();
        return Ok(chapters);
    }
}