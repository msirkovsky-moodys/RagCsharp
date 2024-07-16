using System.Text;
using Core;
using Core.Ollama;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RagExampleController(IPullRequestAgent pullRequestAgent) : ControllerBase
{
    [HttpPost(Name = "Start")]
    public async Task<Suggestion[]> Start([FromBody] PullRequestRequest request)
    {
        var response = await pullRequestAgent.Run(new PrRequestInfo(request.PRNumber, request.RepoName, request.GitToken, request.Prompt));
        
        return response;
    }
}


public class ILLMClient
{
}

public class LLMClient
{
    public void SaveTextEmbedding(string[] documents)
    {
        foreach (var document in documents)
        {
            SaveTextEmbedding(document);
        }
    }

    private void SaveTextEmbedding(string documents)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://127.0.0.1:8000");
        var content = new StringContent(documents, Encoding.UTF8, "application/json");



    }
}