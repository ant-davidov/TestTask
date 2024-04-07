

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask;
using TestTask.Application.Interfaces;
using TestTask.Application.Services.DataProvider;
using TestTask.Application.Services.FileAccess;
using TestTask.Presentation;

class Program
{

    static void Main(string[] args)
    {
        var services = ConfigureServices(args);
        var serviceProvider = services.BuildServiceProvider();
        ServiceHelper.Initialize(serviceProvider);
        var app = new App();
        app.Run();
    }
    private static IServiceCollection ConfigureServices(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        var config = LoadConfiguration();
        if (Array.Exists(args, element => element.ToLower() == "--from-config"))
            services.AddSingleton<IDataProvider>(provider => new JsonDataProvider(config));
        else
            services.AddSingleton<IDataProvider>(provider => new ConsoleDataProvider(args));
        services.AddSingleton<IWrite, WriteToFile>();
        return services;
    }
    public static IConfiguration LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);
        return builder.Build();
    }
}


