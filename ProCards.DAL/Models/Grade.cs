namespace ProCards.DAL.Models;

public class Grade: ModelBase
{
    public int GradeNumber { get; set; }
    
    public Card? Card { get; set; }
    
    public DateTime? PublishedAt { get; set; }
}