namespace ProCards.DAL.Models;

public class Card: ModelBase
{
    public int Id { get; set; }
    
    public string? FirstSide { get; set; }
    
    public string? SecondSide { get; set; }
    
    public Category? Category { get; set; }
    
    public DateTime PublishedAt { get; set; }
}