using Core.GitHubIntegration;

namespace Core;

public class App(IPullRequestProvider pullRequestProvider)
{
    public async Task Run()
    {
        try
        {
            await pullRequestProvider.GetPullRequest(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}