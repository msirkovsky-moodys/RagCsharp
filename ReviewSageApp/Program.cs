using Microsoft.Extensions.DependencyInjection;

namespace ReviewSageApp;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var app = serviceProvider.GetService<App>();
        app!.Run();
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<IMyService, MyService>();
        services.AddSingleton<App>();
    }
}

public class App
{
    private readonly IMyService _myService;

    public App(IMyService myService)
    {
        _myService = myService;
    }

    public void Run()
    {
        _myService.Run();
    }
}

public interface IMyService
{
    void Run();
}

public class MyService : IMyService
{
    public void Run()
    {
        Console.WriteLine("MyService is running");
    }
}