using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.RagExample;

public interface ILLMClient
{
    Task SaveTextEmbedding(SaveFileRequest[] documents);

    Task<string> Query(string prompt);
}

public class LLMClient : ILLMClient
{
    private const string PythonAppUrl = "http://127.0.0.1:8000";

    public async Task SaveTextEmbedding(SaveFileRequest[] documents)
    {
        foreach (var document in documents)
        {
            await SaveTextEmbedding(document);
        }
    }

    public async Task<string> Query(string prompt)
    {
        var request = new QueryRequest(prompt);
        var client = new HttpClient();
        client.BaseAddress = new Uri(PythonAppUrl);
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/query", content);
        return await response.Content.ReadAsStringAsync();
    }

    private async Task SaveTextEmbedding(SaveFileRequest document)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(PythonAppUrl);
        var json = JsonSerializer.Serialize(document);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await client.PostAsync("/save-text-embedding", content);
    }
}

public class QueryRequest(string message)
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = message;
}