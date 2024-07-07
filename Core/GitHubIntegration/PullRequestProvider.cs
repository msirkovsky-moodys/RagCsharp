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
        //var tokenFromEnv = Environment.GetEnvironmentVariable("personal_token_github_4475", EnvironmentVariableTarget.User);
        if (tokenFromEnv == null)
        {
            throw new InvalidOperationException("Github Token not found");
        }

        // Replace with the owner and repository name
        const string owner = "msirkovsky-moodys";
        const string repo = "llm-workshop";

        // GitHub API URL for pull requests
        var url = $"https://api.github.com/repos/{owner}/{repo}/pulls/{id}";

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


Pokracovat
    {
    "url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/1",
    "id": 1945815261,
    "node_id": "PR_kwDOMPmLAM5z-sjd",
    "html_url": "https://github.com/msirkovsky-moodys/llm-workshop/pull/1",
    "diff_url": "https://github.com/msirkovsky-moodys/llm-workshop/pull/1.diff",
    "patch_url": "https://github.com/msirkovsky-moodys/llm-workshop/pull/1.patch",
    "issue_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/1",
    "number": 1,
    "state": "open",
    "locked": false,
    "title": "Test",
    "user": {
        "login": "msirkovsky-moodys",
        "id": 140503865,
        "node_id": "U_kgDOCF_rOQ",
        "avatar_url": "https://avatars.githubusercontent.com/u/140503865?v=4",
        "gravatar_id": "",
        "url": "https://api.github.com/users/msirkovsky-moodys",
        "html_url": "https://github.com/msirkovsky-moodys",
        "followers_url": "https://api.github.com/users/msirkovsky-moodys/followers",
        "following_url": "https://api.github.com/users/msirkovsky-moodys/following{/other_user}",
        "gists_url": "https://api.github.com/users/msirkovsky-moodys/gists{/gist_id}",
        "starred_url": "https://api.github.com/users/msirkovsky-moodys/starred{/owner}{/repo}",
        "subscriptions_url": "https://api.github.com/users/msirkovsky-moodys/subscriptions",
        "organizations_url": "https://api.github.com/users/msirkovsky-moodys/orgs",
        "repos_url": "https://api.github.com/users/msirkovsky-moodys/repos",
        "events_url": "https://api.github.com/users/msirkovsky-moodys/events{/privacy}",
        "received_events_url": "https://api.github.com/users/msirkovsky-moodys/received_events",
        "type": "User",
        "site_admin": false
    },
    "body": null,
    "created_at": "2024-06-30T05:58:52Z",
    "updated_at": "2024-06-30T05:58:52Z",
    "closed_at": null,
    "merged_at": null,
    "merge_commit_sha": "7b7371a43fd5806bd6c2e0b170f22dc1d22d96ca",
    "assignee": null,
    "assignees": [],
    "requested_reviewers": [],
    "requested_teams": [],
    "labels": [],
    "milestone": null,
    "draft": false,
    "commits_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/1/commits",
    "review_comments_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/1/comments",
    "review_comment_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/comments{/number}",
    "comments_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/1/comments",
    "statuses_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/statuses/e5cd04c9d1005804f91e53c6c5d1df82833e51d9",
    "head": {
        "label": "msirkovsky-moodys:test",
        "ref": "test",
        "sha": "e5cd04c9d1005804f91e53c6c5d1df82833e51d9",
        "user": {
            "login": "msirkovsky-moodys",
            "id": 140503865,
            "node_id": "U_kgDOCF_rOQ",
            "avatar_url": "https://avatars.githubusercontent.com/u/140503865?v=4",
            "gravatar_id": "",
            "url": "https://api.github.com/users/msirkovsky-moodys",
            "html_url": "https://github.com/msirkovsky-moodys",
            "followers_url": "https://api.github.com/users/msirkovsky-moodys/followers",
            "following_url": "https://api.github.com/users/msirkovsky-moodys/following{/other_user}",
            "gists_url": "https://api.github.com/users/msirkovsky-moodys/gists{/gist_id}",
            "starred_url": "https://api.github.com/users/msirkovsky-moodys/starred{/owner}{/repo}",
            "subscriptions_url": "https://api.github.com/users/msirkovsky-moodys/subscriptions",
            "organizations_url": "https://api.github.com/users/msirkovsky-moodys/orgs",
            "repos_url": "https://api.github.com/users/msirkovsky-moodys/repos",
            "events_url": "https://api.github.com/users/msirkovsky-moodys/events{/privacy}",
            "received_events_url": "https://api.github.com/users/msirkovsky-moodys/received_events",
            "type": "User",
            "site_admin": false
        },
        "repo": {
            "id": 821660416,
            "node_id": "R_kgDOMPmLAA",
            "name": "llm-workshop",
            "full_name": "msirkovsky-moodys/llm-workshop",
            "private": true,
            "owner": {
                "login": "msirkovsky-moodys",
                "id": 140503865,
                "node_id": "U_kgDOCF_rOQ",
                "avatar_url": "https://avatars.githubusercontent.com/u/140503865?v=4",
                "gravatar_id": "",
                "url": "https://api.github.com/users/msirkovsky-moodys",
                "html_url": "https://github.com/msirkovsky-moodys",
                "followers_url": "https://api.github.com/users/msirkovsky-moodys/followers",
                "following_url": "https://api.github.com/users/msirkovsky-moodys/following{/other_user}",
                "gists_url": "https://api.github.com/users/msirkovsky-moodys/gists{/gist_id}",
                "starred_url": "https://api.github.com/users/msirkovsky-moodys/starred{/owner}{/repo}",
                "subscriptions_url": "https://api.github.com/users/msirkovsky-moodys/subscriptions",
                "organizations_url": "https://api.github.com/users/msirkovsky-moodys/orgs",
                "repos_url": "https://api.github.com/users/msirkovsky-moodys/repos",
                "events_url": "https://api.github.com/users/msirkovsky-moodys/events{/privacy}",
                "received_events_url": "https://api.github.com/users/msirkovsky-moodys/received_events",
                "type": "User",
                "site_admin": false
            },
            "html_url": "https://github.com/msirkovsky-moodys/llm-workshop",
            "description": null,
            "fork": false,
            "url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop",
            "forks_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/forks",
            "keys_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/keys{/key_id}",
            "collaborators_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/collaborators{/collaborator}",
            "teams_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/teams",
            "hooks_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/hooks",
            "issue_events_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/events{/number}",
            "events_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/events",
            "assignees_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/assignees{/user}",
            "branches_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/branches{/branch}",
            "tags_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/tags",
            "blobs_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/blobs{/sha}",
            "git_tags_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/tags{/sha}",
            "git_refs_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/refs{/sha}",
            "trees_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/trees{/sha}",
            "statuses_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/statuses/{sha}",
            "languages_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/languages",
            "stargazers_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/stargazers",
            "contributors_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/contributors",
            "subscribers_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/subscribers",
            "subscription_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/subscription",
            "commits_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/commits{/sha}",
            "git_commits_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/commits{/sha}",
            "comments_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/comments{/number}",
            "issue_comment_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/comments{/number}",
            "contents_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/contents/{+path}",
            "compare_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/compare/{base}...{head}",
            "merges_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/merges",
            "archive_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/{archive_format}{/ref}",
            "downloads_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/downloads",
            "issues_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues{/number}",
            "pulls_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls{/number}",
            "milestones_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/milestones{/number}",
            "notifications_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/notifications{?since,all,participating}",
            "labels_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/labels{/name}",
            "releases_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/releases{/id}",
            "deployments_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/deployments",
            "created_at": "2024-06-29T04:50:19Z",
            "updated_at": "2024-06-30T14:16:58Z",
            "pushed_at": "2024-06-30T14:16:55Z",
            "git_url": "git://github.com/msirkovsky-moodys/llm-workshop.git",
            "ssh_url": "git@github.com:msirkovsky-moodys/llm-workshop.git",
            "clone_url": "https://github.com/msirkovsky-moodys/llm-workshop.git",
            "svn_url": "https://github.com/msirkovsky-moodys/llm-workshop",
            "homepage": null,
            "size": 367,
            "stargazers_count": 0,
            "watchers_count": 0,
            "language": "Python",
            "has_issues": true,
            "has_projects": true,
            "has_downloads": true,
            "has_wiki": false,
            "has_pages": false,
            "has_discussions": false,
            "forks_count": 0,
            "mirror_url": null,
            "archived": false,
            "disabled": false,
            "open_issues_count": 1,
            "license": null,
            "allow_forking": true,
            "is_template": false,
            "web_commit_signoff_required": false,
            "topics": [],
            "visibility": "private",
            "forks": 0,
            "open_issues": 1,
            "watchers": 0,
            "default_branch": "master"
        }
    },
    "base": {
        "label": "msirkovsky-moodys:master",
        "ref": "master",
        "sha": "334b31fab52604c3bf69a41f8625a9e51087b116",
        "user": {
            "login": "msirkovsky-moodys",
            "id": 140503865,
            "node_id": "U_kgDOCF_rOQ",
            "avatar_url": "https://avatars.githubusercontent.com/u/140503865?v=4",
            "gravatar_id": "",
            "url": "https://api.github.com/users/msirkovsky-moodys",
            "html_url": "https://github.com/msirkovsky-moodys",
            "followers_url": "https://api.github.com/users/msirkovsky-moodys/followers",
            "following_url": "https://api.github.com/users/msirkovsky-moodys/following{/other_user}",
            "gists_url": "https://api.github.com/users/msirkovsky-moodys/gists{/gist_id}",
            "starred_url": "https://api.github.com/users/msirkovsky-moodys/starred{/owner}{/repo}",
            "subscriptions_url": "https://api.github.com/users/msirkovsky-moodys/subscriptions",
            "organizations_url": "https://api.github.com/users/msirkovsky-moodys/orgs",
            "repos_url": "https://api.github.com/users/msirkovsky-moodys/repos",
            "events_url": "https://api.github.com/users/msirkovsky-moodys/events{/privacy}",
            "received_events_url": "https://api.github.com/users/msirkovsky-moodys/received_events",
            "type": "User",
            "site_admin": false
        },
        "repo": {
            "id": 821660416,
            "node_id": "R_kgDOMPmLAA",
            "name": "llm-workshop",
            "full_name": "msirkovsky-moodys/llm-workshop",
            "private": true,
            "owner": {
                "login": "msirkovsky-moodys",
                "id": 140503865,
                "node_id": "U_kgDOCF_rOQ",
                "avatar_url": "https://avatars.githubusercontent.com/u/140503865?v=4",
                "gravatar_id": "",
                "url": "https://api.github.com/users/msirkovsky-moodys",
                "html_url": "https://github.com/msirkovsky-moodys",
                "followers_url": "https://api.github.com/users/msirkovsky-moodys/followers",
                "following_url": "https://api.github.com/users/msirkovsky-moodys/following{/other_user}",
                "gists_url": "https://api.github.com/users/msirkovsky-moodys/gists{/gist_id}",
                "starred_url": "https://api.github.com/users/msirkovsky-moodys/starred{/owner}{/repo}",
                "subscriptions_url": "https://api.github.com/users/msirkovsky-moodys/subscriptions",
                "organizations_url": "https://api.github.com/users/msirkovsky-moodys/orgs",
                "repos_url": "https://api.github.com/users/msirkovsky-moodys/repos",
                "events_url": "https://api.github.com/users/msirkovsky-moodys/events{/privacy}",
                "received_events_url": "https://api.github.com/users/msirkovsky-moodys/received_events",
                "type": "User",
                "site_admin": false
            },
            "html_url": "https://github.com/msirkovsky-moodys/llm-workshop",
            "description": null,
            "fork": false,
            "url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop",
            "forks_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/forks",
            "keys_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/keys{/key_id}",
            "collaborators_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/collaborators{/collaborator}",
            "teams_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/teams",
            "hooks_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/hooks",
            "issue_events_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/events{/number}",
            "events_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/events",
            "assignees_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/assignees{/user}",
            "branches_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/branches{/branch}",
            "tags_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/tags",
            "blobs_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/blobs{/sha}",
            "git_tags_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/tags{/sha}",
            "git_refs_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/refs{/sha}",
            "trees_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/trees{/sha}",
            "statuses_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/statuses/{sha}",
            "languages_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/languages",
            "stargazers_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/stargazers",
            "contributors_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/contributors",
            "subscribers_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/subscribers",
            "subscription_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/subscription",
            "commits_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/commits{/sha}",
            "git_commits_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/git/commits{/sha}",
            "comments_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/comments{/number}",
            "issue_comment_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/comments{/number}",
            "contents_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/contents/{+path}",
            "compare_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/compare/{base}...{head}",
            "merges_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/merges",
            "archive_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/{archive_format}{/ref}",
            "downloads_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/downloads",
            "issues_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues{/number}",
            "pulls_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls{/number}",
            "milestones_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/milestones{/number}",
            "notifications_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/notifications{?since,all,participating}",
            "labels_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/labels{/name}",
            "releases_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/releases{/id}",
            "deployments_url": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/deployments",
            "created_at": "2024-06-29T04:50:19Z",
            "updated_at": "2024-06-30T14:16:58Z",
            "pushed_at": "2024-06-30T14:16:55Z",
            "git_url": "git://github.com/msirkovsky-moodys/llm-workshop.git",
            "ssh_url": "git@github.com:msirkovsky-moodys/llm-workshop.git",
            "clone_url": "https://github.com/msirkovsky-moodys/llm-workshop.git",
            "svn_url": "https://github.com/msirkovsky-moodys/llm-workshop",
            "homepage": null,
            "size": 367,
            "stargazers_count": 0,
            "watchers_count": 0,
            "language": "Python",
            "has_issues": true,
            "has_projects": true,
            "has_downloads": true,
            "has_wiki": false,
            "has_pages": false,
            "has_discussions": false,
            "forks_count": 0,
            "mirror_url": null,
            "archived": false,
            "disabled": false,
            "open_issues_count": 1,
            "license": null,
            "allow_forking": true,
            "is_template": false,
            "web_commit_signoff_required": false,
            "topics": [],
            "visibility": "private",
            "forks": 0,
            "open_issues": 1,
            "watchers": 0,
            "default_branch": "master"
        }
    },
    "_links": {
        "self": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/1"
        },
        "html": {
            "href": "https://github.com/msirkovsky-moodys/llm-workshop/pull/1"
        },
        "issue": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/1"
        },
        "comments": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/issues/1/comments"
        },
        "review_comments": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/1/comments"
        },
        "review_comment": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/comments{/number}"
        },
        "commits": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/pulls/1/commits"
        },
        "statuses": {
            "href": "https://api.github.com/repos/msirkovsky-moodys/llm-workshop/statuses/e5cd04c9d1005804f91e53c6c5d1df82833e51d9"
        }
    },
    "author_association": "OWNER",
    "auto_merge": null,
    "active_lock_reason": null,
    "merged": false,
    "mergeable": true,
    "rebaseable": true,
    "mergeable_state": "clean",
    "merged_by": null,
    "comments": 0,
    "review_comments": 0,
    "maintainer_can_modify": false,
    "commits": 2,
    "additions": 35,
    "deletions": 0,
    "changed_files": 2
}