using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProCards.DAL;

namespace ProCards.Web.Data.DTOs;

public class CategoryDto
{
    [JsonProperty(PropertyName ="id")]
    public int? Id { get; init; }

    [Required]
    [JsonProperty(PropertyName ="name")]
    [MaxLength(ConfigurationConstants.MaxCategoryNameLength)]
    public string Name { get; init; }

    [Required]
    [JsonProperty(PropertyName ="isUserCategory")]
    public bool? IsUserCategory { get; init; }
}