using System.ComponentModel.DataAnnotations;

namespace ProCards.DAL.Models;

public class Grade: ModelBase
{
    [Required]
    public int GradeNumber { get; set; }
    
    [Required]
    public Card? Card { get; set; }
    
    public DateTime PublishedAt { get; set; }
}