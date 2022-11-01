using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProCards.Core.Data.DTOs;

public class CategoryDto
{
    [Required]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}