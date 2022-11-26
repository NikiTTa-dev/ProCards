using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProCards.DAL;

namespace ProCards.Web.Data.DTOs;

public class CategoryDto
{
    [JsonProperty(PropertyName ="id")]
    public int? Id { get; init; }

    [JsonProperty(PropertyName ="name")]
    public string Name { get; init; }

    [JsonProperty(PropertyName ="isUserCategory")]
    public bool? IsUserCategory { get; init; }
}