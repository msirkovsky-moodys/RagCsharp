using System.Text;
using System.Text.Json;

namespace Core.Ollama;

public interface IOllamaProvider
{
    Task<string> CallOllama(string query);
}

public class OllamaProvider : IOllamaProvider
{
    private const string OllamaEndpoint = "http://localhost:11434/api/chat";

    public async Task<string> CallOllama(string query)
    {
        using var client = new HttpClient();
        // Create the payload
        var payload = new OllamaInput
        {
            model = "mario",
            messages =
            [
                new OllamaMessage
                {
                    role= "user",
                    content= query,
                    
                }
            ],
            stream = false,
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(OllamaEndpoint, content);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<OllamamResponse>(responseContent);
            return jsonResponse!.message.content;
        }

        return $"Error: {response.StatusCode} - {response.ReasonPhrase}";

    }

}
