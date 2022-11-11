using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProCards.DAL;

namespace ProCards.Web.Data.DTOs;

public class CategoryDto
{
    [Required]
    [JsonPropertyName("name")]
    [MaxLength(ConfigurationConstants.MaxCategoryNameLength)]
    public string? Name { get; init; }
    
    [Required]
    [JsonPropertyName("isUserCategory")]
    public bool IsUserCategory { get; init; }
}