using FoiaOnline.App;
using FoiaOnline.Client;
using FoiaOnline.Data;
using FoiaOnline.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((hostingContext, loggingBuilder) =>
    {
        loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<App>();

        services.Configure<AppSettings>(hostContext.Configuration.GetSection(nameof(AppSettings)));

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(hostContext.Configuration.GetConnectionString("ApplicationDbContext")));

        services.AddSingleton<RequestService>();
        services.AddSingleton<FoiaClient>();

    })
    .Build();

await host.StartAsync();