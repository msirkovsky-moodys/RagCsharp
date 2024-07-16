using System.Text.Json.Serialization;

namespace Core.RagExample;

public class SaveFileRequest
{
    [JsonPropertyName("filename")]
    public required string FileName { get; set; }
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}