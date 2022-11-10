namespace ProCards.DAL.Models;

public class CategoryDal: ModelBase
{
    public string? Name { get; init; }
    
    public bool IsUserCategory { get; init; }
    
    public ICollection<CardDal>? Cards { get; init; }
    
    public DateTime? PublishedAt { get; init; }
}