using System.ComponentModel.DataAnnotations;

namespace ProCards.Infrastructure.Models;

public class Card
{
    public int Id { get; set; }
    
    [Required]
    public string? FirstSide { get; set; }
    
    [Required]
    public string? SecondSide { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
}