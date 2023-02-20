using MadinahArabic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadinahArabic.Persistence.DbContextConfiguration;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasMany(l => l.Blocks)
            .WithOne(b => b.Lesson)
            .HasForeignKey(b => b.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}