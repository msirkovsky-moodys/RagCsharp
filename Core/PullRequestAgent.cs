using Core.GitHubIntegration;
using Core.Ollama;

namespace Core;

public record PrRequestInfo(int PullRequestId, string RepoName, string Token, string Prompt);

public interface IPullRequestAgent
{
    Task<Suggestion[]> Run(PrRequestInfo prRequestInfo);

}
public class PullRequestAgent(
    IPullRequestProvider pullRequestProvider,
    IOllamaProvider ollamaProvider
    ) : IPullRequestAgent
{
    public async Task<Suggestion[]> Run(PrRequestInfo prRequestInfo)
    {
        if (prRequestInfo.PullRequestId == -1)
        {
            Thread.Sleep(2000);
            return
            [
                new Suggestion
                {
                    OriginalCode = "var test = 1;",
                    NewCode = "const int test = 1;",
                    FileName = "test.cs"
                },
                new Suggestion
                {
                    OriginalCode = "var test = 2;",
                    NewCode = "const int test = 2;",
                    FileName = "test2.cs"
                },
                new Suggestion
                {
                    OriginalCode = "var test = 3;",
                    NewCode = "const int test = 3;",
                    FileName = "test3.cs"
                }
            ];
        }

        if (string.IsNullOrWhiteSpace(prRequestInfo.Token))
        {
            prRequestInfo = prRequestInfo with
            {
                Token = Environment.GetEnvironmentVariable("personal_token_github_4475", EnvironmentVariableTarget.Machine)!
            };
        }

        var info = await pullRequestProvider.GetPullRequest(prRequestInfo.PullRequestId, prRequestInfo.RepoName, prRequestInfo.Token);

        var infoCsharp = info.Parts
            .Where(x => x.FileName.EndsWith(".cs"))
            .ToArray();

        var tasks = infoCsharp.Select(chunk => CallOllama(chunk, prRequestInfo.Prompt));
        
        // I want to call one by one on the local machine. Parallel can be used with Moody's copilot.
        var results = new List<Suggestion>();
        foreach (var task in tasks)
        {
            results.Add(await task);
        }
        //use this in Production:
        //var results = await Task.WhenAll(resultTasks); 

        return results.OrderBy(x => x.FileName).ToArray();
    }

    private async Task<Suggestion> CallOllama(FilePatchInfo filePatchInfo, string prompt)
    {
        //var prompt = $"""
        //    My C# code:
        //    {filePatchInfo.PatchInfo.AddedOrModifiedCode}
        //    ---
        //    please improve it to conform these rules:
        //    var plus value type should be write as const value type.
        //    And also add anything you think it's worth improving.
        //    """;

        var reply = await ollamaProvider.CallOllama(prompt);
        return new Suggestion
        {
            OriginalCode = filePatchInfo.PatchInfo.AddedOrModifiedCode,
            NewCode = reply,
            FileName = filePatchInfo.FileName
        };
    }

    //private async Task TestOllama()
    //{
    //    const string prompt = """
    //      I have this C# code:
    //      var test = 1;
    //      ---
    //      please improve it to conform these rules:
    //      var plus value type should be write as const value type.
    //      And also add anything you think it's worth improving.
    //      """;
    //    var reply = await ollamaProvider.CallOllama(prompt);
    //}
}

public class Suggestion
{
    public required string OriginalCode { get; set; }
    public required string NewCode { get; set; }
    public required string FileName { get; set; }
}