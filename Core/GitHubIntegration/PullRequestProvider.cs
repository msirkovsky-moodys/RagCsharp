using System.Net.Http.Headers;
using System.Text.Json;


namespace Core.GitHubIntegration;

public interface IPullRequestProvider
{
    Task<PullRequestInfo> GetPullRequest(int id);
}

public record PullRequestInfo(string Title, string[] Parts);


public class PullRequestProvider : IPullRequestProvider
{
    public async Task<PullRequestInfo> GetPullRequest(int id)
    {
        var tokenFromEnv = Environment.GetEnvironmentVariable("personal_token_github_4475");
        if (tokenFromEnv == null)
        {
            throw new InvalidOperationException("Github Token not found");
        }

        // Replace with the owner and repository name
        const string owner = "msirkovsky-moodys";
        const string repo = "llm-workshop";

        // GitHub API URL for pull requests
        const string url = $"https://api.github.com/repos/{owner}/{repo}/pulls";

        using HttpClient client = new HttpClient();
        // Headers for authentication and content type
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", tokenFromEnv);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("HttpClient", "1.0"));

        // Make the GET request to fetch pull requests
        HttpResponseMessage response = await client.GetAsync(url);

        // Check if the request was successful
        if (response.IsSuccessStatusCode) 
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializer.Deserialize<PullRequestInfo>(responseBody);
            //foreach (var pr in pullRequests)
            //{
            //    Console.WriteLine($"PR Title: {pr["title"]}");
            //    Console.WriteLine($"PR Number: {pr["number"]}");
            //    Console.WriteLine($"PR State: {pr["state"]}");
            //    Console.WriteLine($"PR URL: {pr["html_url"]}");
            //    Console.WriteLine();
            //}
        }
        else
        {
            Console.WriteLine($"Failed to fetch pull requests: {response.StatusCode}");
        }

        return new PullRequestInfo("Title", [
            "Part1", "Part2"
        ]);
    }

}