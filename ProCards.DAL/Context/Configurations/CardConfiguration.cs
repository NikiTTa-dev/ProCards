using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCards.DAL.Models;

namespace ProCards.DAL.Context.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.Property(p => p.FirstSide)
            .HasMaxLength(ConfigurationConstants.MaxCardFirstSideLength)
            .IsRequired();
        builder.Property(p => p.SecondSide)
            .HasMaxLength(ConfigurationConstants.MaxCardSecondSideLength)
            .IsRequired();
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Cards)
            .IsRequired();
    }
}