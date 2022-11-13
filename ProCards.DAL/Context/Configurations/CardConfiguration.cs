using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCards.DAL.Models;

namespace ProCards.DAL.Context.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<CardDal>
{
    public void Configure(EntityTypeBuilder<CardDal> builder)
    {
        builder.HasKey(p => p.FirstSide);
        builder.Property(p => p.FirstSide)
            .HasMaxLength(ConfigurationConstants.MaxCardSideLength)
            .IsRequired();
        builder.Property(p => p.SecondSide)
            .HasMaxLength(ConfigurationConstants.MaxCardSideLength)
            .IsRequired();
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Cards)
            .IsRequired();
        builder.HasMany(p => p.Grades)
            .WithOne(c => c.Card);
    }
}