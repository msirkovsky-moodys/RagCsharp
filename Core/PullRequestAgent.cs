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
    public async Task<Suggestion[]> Run(int pullRequestId, string repoName, string? token)
    {
        if (pullRequestId == -1)
        {
            Thread.Sleep(2000);
            return
            [
                new Suggestion
                {
                    OriginalCode = "var test = 1;",
                    NewCode = "const int test = 1;",
                    FileName = "test.cs"
                }
            ];
        }
        if (string.IsNullOrWhiteSpace(token))
            token = Environment.GetEnvironmentVariable("personal_token_github_4475", EnvironmentVariableTarget.Machine);

        var info = await pullRequestProvider.GetPullRequest(pullRequestId, repoName, token!);

        var infoCsharp = info.Parts
            .Where(x => x.FileName.EndsWith(".cs"))
            .ToArray();

        var resultTasks = infoCsharp.Select(CallOllama);
        
        var results = await Task.WhenAll(resultTasks);
        return results.OrderBy(x => x.FileName).ToArray();
    }

    private async Task<Suggestion> CallOllama(FilePatchInfo filePatchInfo)
    {
        var prompt = $"""
            I have this C# code:
            {filePatchInfo.PatchInfo.AddedOrModifiedCode}
            ---
            please improve it to conform these rules:
            var plus value type should be write as const value type.
            And also add anything you think it's worth improving.
            """;

        var reply = await ollamaProvider.CallOllama(prompt);
        return new Suggestion
        {
            OriginalCode = filePatchInfo.PatchInfo.AddedOrModifiedCode,
            NewCode = reply,
            FileName = filePatchInfo.FileName
        };
    }

    private async Task TestOllama()
    {
        const string prompt = """
          I have this C# code:
          var test = 1;
          ---
          please improve it to conform these rules:
          var plus value type should be write as const value type.
          And also add anything you think it's worth improving.
          """;
        var reply = await ollamaProvider.CallOllama(prompt);
    }
}

public class Suggestion
{
    public required string OriginalCode { get; set; }
    public required string NewCode { get; set; }
    public required string FileName { get; set; }
}