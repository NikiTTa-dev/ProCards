using System.Diagnostics.CodeAnalysis;

namespace ProCards.DAL.Models;

public class CardDal
{
    public string? FirstSide { get; init; }
    
    public string? SecondSide { get; init; }
    
    [NotNull]
    public CategoryDal? Category { get; init; }
    
    public DateTime? PublishedAt { get; init; }
    
    public ICollection<GradeDal>? Grades { get; init; }
}