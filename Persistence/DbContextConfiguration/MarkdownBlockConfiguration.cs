using MadinahArabic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadinahArabic.Persistence.DbContextConfiguration;

public class MarkdownBlockConfiguration : IEntityTypeConfiguration<MarkdownBlock>
{
    public void Configure(EntityTypeBuilder<MarkdownBlock> builder)
    {
        builder.Property(t => t.Content)
            .HasColumnType("text");
    }
}