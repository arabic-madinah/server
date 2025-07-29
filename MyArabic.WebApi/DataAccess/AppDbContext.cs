using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.Models;

namespace MyArabic.WebApi.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chapter>()
            .HasMany(c => c.Lessons)
            .WithOne(l => l.Chapter)
            .HasForeignKey(l => l.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Chapter>()
            .HasIndex(x => x.Slug)
            .IsUnique();

        modelBuilder.Entity<Lesson>()
            .HasIndex(x => x.Slug)
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}