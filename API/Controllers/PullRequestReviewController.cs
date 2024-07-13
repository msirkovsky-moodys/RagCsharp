using Core;
using Core.Ollama;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PullRequestReviewController(IPullRequestAgent pullRequestAgent) : ControllerBase
{
    [HttpPost(Name = "Start")]
    public async Task<Suggestion[]> Start([FromBody] PullRequestRequest request)
    {
        var response = await pullRequestAgent.Run(new PrRequestInfo(request.PRNumber, request.RepoName, request.GitToken, request.Prompt));
        
        return response;
    }
}

public class PullRequestRequest
{
    public required int PRNumber { get; set; }
    public required string RepoName { get; set; }
    public required string GitToken { get; set; }
    public required string Prompt{ get; set; }
}