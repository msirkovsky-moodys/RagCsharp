using System.Text;
using Core;
using Core.Ollama;
using Core.RagExample;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RagExampleController(ILLMClient llmClient) : ControllerBase
{
    [HttpPost("/Query")]
    public async Task<string> Query([FromBody] QueryRequest request)
    {
        var response = await llmClient.Query(request.Message);
        return response;
    }

    [HttpPost("/IngestFiles")]
    public async Task<bool> IngestFiles([FromBody] SaveFileRequest request)
    {
        await llmClient.SaveTextEmbedding([request]);
        return true;
    }

}
