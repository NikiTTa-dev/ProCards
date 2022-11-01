using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProCards.Core.Data.DTOs;

public class CardDto
{
    [Required]
    [JsonPropertyName("firstSide")]
    public string? FirstSide { get; set; }
    
    [Required]
    [JsonPropertyName("secondSide")]
    public string? SecondSide { get; set; }
    
    [Required]
    [JsonPropertyName("cardCategory")]
    public CategoryDto? CardCategory { get; set; }
}