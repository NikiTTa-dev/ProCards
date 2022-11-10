using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCards.DAL.Models;

namespace ProCards.DAL.Context.Configurations;

public class GradeConfiguration: IEntityTypeConfiguration<GradeDal>
{
    public void Configure(EntityTypeBuilder<GradeDal> builder)
    {
        builder.Property(p => p.GradeNumber)
            .IsRequired();
        builder.HasOne(p => p.Card)
            .WithMany(g => g.Grades)
            .IsRequired();
    }
}