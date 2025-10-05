using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task3.ConsoleApp;
using Task3.DataAccess.Connection;
using Task3.DataAccess.Repositories;
using ProjectTask = Task3.DataAccess.Models.Task;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<App>();
        services.AddTransient<Worker>();
        services.AddTransient<IRepository<ProjectTask>, TaskRepository>();
        services.AddSingleton<IConnectionFactory, SqlServerConnectionFactory>();
    })
    .Build();

var app = host.Services.GetService<App>()
        ?? throw new Exception("Couldn't run program");

await app.Run();
