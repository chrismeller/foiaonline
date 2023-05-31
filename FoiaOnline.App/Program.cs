using FoiaOnline.App;
using FoiaOnline.Client;
using FoiaOnline.Data;
using FoiaOnline.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<App>();

        services.Configure<AppSettings>(hostContext.Configuration.GetSection(nameof(AppSettings)));

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(hostContext.Configuration.GetConnectionString("ApplicationDbContext")));

        services.AddSingleton<RequestService>();
        services.AddSingleton<FoiaClient>();

    })
    .Build();