using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCards.DAL.Models;

namespace ProCards.DAL.Context.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(ConfigurationConstants.MaxCategoryNameLength)
            .IsRequired();
        builder.Property(p => p.IsUserCategory)
            .IsRequired();
        builder.HasMany(p => p.Cards)
            .WithOne(c => c.Category);
    }
}