using System.Diagnostics.CodeAnalysis;

namespace ProCards.DAL.Models;

public class CategoryDal: ModelBase
{
    [NotNull]
    public string? Name { get; init; }
    
    public bool IsUserCategory { get; init; }
    
    public ICollection<CardDal>? Cards { get; set; }
    
    public DateTime? PublishedAt { get; init; }
}