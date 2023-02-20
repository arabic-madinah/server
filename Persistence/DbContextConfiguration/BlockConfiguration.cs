using MadinahArabic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadinahArabic.Persistence.DbContextConfiguration;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasDiscriminator(x => x.Type)
            .HasValue<MarkdownBlock>("markdown")
            .HasValue<QuizBlock>("quiz");
    }
}