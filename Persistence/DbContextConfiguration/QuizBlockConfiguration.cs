using MadinahArabic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadinahArabic.Persistence.DbContextConfiguration;

public class QuizBlockConfiguration : IEntityTypeConfiguration<QuizBlock>
{
    public void Configure(EntityTypeBuilder<QuizBlock> builder)
    {
        builder.OwnsMany(
            q => q.Options, o => 
            { 
                o.WithOwner().HasForeignKey(x => x.QuizBlockId);
                o.HasKey(x => new { x.QuizBlockId, x.Id });
            });
    }
}