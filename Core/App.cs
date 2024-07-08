using Core.GitHubIntegration;

namespace Core;

public class App(IPullRequestProvider pullRequestProvider)
{
    public async Task Run()
    {
        try
        {
            var info = await pullRequestProvider.GetPullRequest(1, "PullRequestReviewTest");
            var infoCsharp =  info.Parts.Where(x => x.FileName.EndsWith(".cs")).ToArray();

            foreach (var filePatchInfo in infoCsharp)
            {
                var codeToImprove = filePatchInfo.PatchInfo.AddedOrModifiedCode;
                Console.WriteLine("Improving:" + codeToImprove);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}