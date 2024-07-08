using System.Text.Json.Serialization;

namespace Core.GitHubIntegration;

public record PullRequestInfo(string Title, FilePatchInfo[] Parts);

public class FilePatchInfo
{
    [JsonPropertyName("filename")]
    public required string FileName { get; set; }

    [JsonPropertyName("patch")]
    public required string Patch { get; set; }

    public PatchInfo PatchInfo => new(Patch, PatchParser.ExtractAddedLines(Patch));
}

public record PatchInfo(string Raw, string AddedOrModifiedCode);
