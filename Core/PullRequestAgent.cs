using Core.GitHubIntegration;
using Core.Ollama;

namespace Core;

public interface IPullRequestAgent
{
    Task<Suggestion[]> Run(int pullRequestId, string repoName, string token);

}
public class PullRequestAgent(
    IPullRequestProvider pullRequestProvider,
    IOllamaProvider ollamaProvider
    ) : IPullRequestAgent
{
    public async Task<Suggestion[]> Run(int pullRequestId, string repoName, string token)
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

        //var prompt = await ollamaProvider.CallOllama("What is the weather in London?");
        var prompt = """
            I have this C# code:
            var test = 1;
            ---
            please improve it to conform these rules:
            var plus value type should be write as const value type.
            And also add anything you think it's worth improving.
            """;
        //var reply = await ollamaProvider.CallOllama(prompt);
        var reply = "Test";

        return [new Suggestion() { OriginalCode = "var test = 1;", NewCode = reply }];
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

public class Suggestion
{
    public required string OriginalCode { get; set; }
    public required string NewCode { get; set; }
}