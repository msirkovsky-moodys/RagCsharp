using Core.GitHubIntegration;
using Core.Ollama;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DI;

public class Registering
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IPullRequestProvider, PullRequestProvider>();
        services.AddSingleton<IOllamaProvider, OllamaProvider>();
    }
}