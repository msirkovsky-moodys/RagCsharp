using System.Net.Http.Headers;
using System.Text.Json;

namespace Core.GitHubIntegration;

public interface IPullRequestProvider
{
    Task<PullRequestInfo> GetPullRequest(int id, string repo);
}

public class PullRequestProvider : IPullRequestProvider
{
    public async Task<PullRequestInfo> GetPullRequest(int id, string repo)
    {
        var tokenFromEnv = Environment.GetEnvironmentVariable("personal_token_github_4475", EnvironmentVariableTarget.Machine);
        if (tokenFromEnv == null)
        {
            throw new InvalidOperationException("Github Token not found");
        }

        // Replace with the owner and repository name
        const string owner = "msirkovsky-moodys";

        // GitHub API URL for pull requests
        var url = $"https://api.github.com/repos/{owner}/{repo}/pulls/{id}/files";

        using var client = new HttpClient();
        // Headers for authentication and content type
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", tokenFromEnv);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("HttpClient", "1.0"));

        // Make the GET request to fetch pull requests
        var response = await client.GetAsync(url);

        // Check if the request was successful
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            return new PullRequestInfo(Title: "PR:" + id,
                Parts: JsonSerializer.Deserialize<FilePatchInfo[]>(responseBody)!
            );
        }
        else
        {
            Console.WriteLine($"Failed to fetch pull requests: {response.StatusCode}");
            throw new InvalidOperationException("Failed to fetch pull requests");
        }
    }
}