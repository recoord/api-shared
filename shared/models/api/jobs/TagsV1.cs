using System.Text.Json.Serialization;

public record TagsV1
{
    [JsonExtensionData]
    public Dictionary<string, object> additionalJSON { get; init; } = new Dictionary<string, object>();
}