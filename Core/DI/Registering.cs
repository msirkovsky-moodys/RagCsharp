using Core.GitHubIntegration;
using Core.Ollama;
using Core.RagExample;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DI;

public class Registering
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IPullRequestProvider, PullRequestProvider>();
        services.AddTransient<IOllamaProvider, OllamaProvider>();
        services.AddTransient<IPullRequestAgent, PullRequestAgent>();
        services.AddTransient<ILLMClient, LLMClient>();
    }
}