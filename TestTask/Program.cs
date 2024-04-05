

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography.X509Certificates;
using TestTask;
using TestTask.Application.Interfaces;
using TestTask.Application.Services.DataProvider;
using TestTask.Presentation;

class Program
{

    static async Task Main(string[] args)
    {

        var services = ConfigureServices(args);
        var serviceProvider = services.BuildServiceProvider();
        ServiceHelper.Initialize(serviceProvider);
        var app = serviceProvider.GetService<App>();
        await app.Run(args, serviceProvider);  
    }

   

    private static IServiceCollection ConfigureServices(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        var config = LoadConfiguration();
        if (Array.Exists(args, element => element.ToLower() == "--from-config"))
            services.AddTransient<IDataProvider>(provider => new JsonDataProvider(config));
        else
            services.AddTransient<IDataProvider>(provider => new ConsoleDataProvider(args));
        services.AddSingleton(provider => new App(config));
        return services;
    }
    public static IConfiguration LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.config.json", optional: true);
        return builder.Build();
    }



}


