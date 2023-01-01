using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutBuddy.TestAuthClient;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.AddHostedService<AuthService>();
    })
    .Build();

host.Run();