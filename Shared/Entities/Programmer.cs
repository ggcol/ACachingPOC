using System.Text.Json.Serialization;

namespace Shared.Entities;

public class Programmer
{
    [JsonPropertyName(nameof(Id))] public int? Id { get; set; }
    [JsonPropertyName(nameof(Name))] public string? Name { get; set; }
}