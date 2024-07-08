using Core.GitHubIntegration;
using Core.Ollama;

namespace Core;

public class App(
    IPullRequestProvider pullRequestProvider,
    IOllamaProvider ollamaProvider
    )
{
    public async Task Run()
    {

        //var info = await GetFiles(1)
        /*
            var infoCsharp =  info.Parts.Where(x => x.FileName.EndsWith(".cs")).ToArray();

            foreach (var filePatchInfo in infoCsharp)
            {
                var codeToImprove = filePatchInfo.PatchInfo.AddedOrModifiedCode;
                Console.WriteLine("Improving:" + codeToImprove);
            }
            */

        var prompt = await ollamaProvider.CallOllama("What is the weather in London?");
        Console.WriteLine(prompt);
    }
    public async Task<PullRequestInfo> GetFiles(int prId)
    {
        try
        {
            var info = await pullRequestProvider.GetPullRequest(prId, "PullRequestReviewTest");
            return info;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}