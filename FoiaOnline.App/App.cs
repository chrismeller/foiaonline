using FoiaOnline.Client;
using FoiaOnline.Domain;
using Microsoft.Extensions.Hosting;

namespace FoiaOnline.App;

public class App : IHostedService
{
    private readonly FoiaClient _client;
    private readonly RequestService _service;

    public App(FoiaClient client, RequestService service)
    {
        _client = client;
        _service = service;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var requests = await _client.GetSearchResult(new DateTime(2022, 1, 1), new DateTime(2022, 1, 8));

        await Console.Out.WriteLineAsync($"Got {requests.data.Length} requests.");

        foreach (var request in requests.data)
        {
            await _service.LogFoundRequest(request.trackingNumber);

            await Console.Out.WriteAsync($"Getting files for request {request.trackingNumber}... ");

            var files = await _client.GetRequestFiles(request.trackingNumber);

            await Console.Out.WriteLineAsync($"Got {files.data.Length}");

            if (files.data.Any())
            {
                
            }
        }
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}