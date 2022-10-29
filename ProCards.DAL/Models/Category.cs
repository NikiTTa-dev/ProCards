namespace ProCards.DAL.Models;

public class Category: ModelBase
{
    public string? Name { get; set; }
    
    public bool IsUserCategory { get; set; }
    
    public ICollection<Card>? Cards { get; set; }
    
    public DateTime? PublishedAt { get; set; }
}