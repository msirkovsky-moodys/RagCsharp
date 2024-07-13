using Core;
using Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace ReviewSageApp;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var app = serviceProvider.GetService<PullRequestAgent>()!;
        await app.Run(new PrRequestInfo(1, "", "", "TODO"));
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<PullRequestAgent>();
        Registering.ConfigureServices(services);
    }
}