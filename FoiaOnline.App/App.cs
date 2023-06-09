using FoiaOnline.Client;
using FoiaOnline.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoiaOnline.App;

public class App : IHostedService
{
    private readonly FoiaClient _client;
    private readonly RequestService _service;
    private readonly ILogger<App> _logger;

    public App(FoiaClient client, RequestService service, ILogger<App> logger)
    {
        _client = client;
        _service = service;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var lastRequestDate = await _service.GetLastFoundRequestDate();

        if (lastRequestDate == null)
        {
            lastRequestDate = new DateTime(2022, 1, 1);
        }

        var keepGoing = true;
        do
        {
            var offset = 0;
            var keepGoingPages = true;
            do
            {
                _logger.LogInformation($"Getting requests for {lastRequestDate.Value:yyyy-MM-dd} to {lastRequestDate.Value.AddDays(7):yyyy-MM-dd} at offset {offset}");

                var requests =
                    await _client.GetSearchResult(lastRequestDate.Value, lastRequestDate.Value.AddDays(7), offset);

                _logger.LogInformation($"Got {requests.data.Length} requests");
                _logger.LogInformation($"There are {requests.recordsTotal} total records.");

                //var requestsToSave = requests.data.ToDictionary(x => x.trackingNumber, x => lastRequestDate.Value);

                //await _service.LogFoundRequests(requestsToSave);

                //_logger.LogInformation($"Saved {requestsToSave.Count} requests");

                foreach (var request in requests.data)
                {
                    _logger.LogDebug($"Logging request {request.trackingNumber} as found.");

                    await _service.LogFoundRequest(request.trackingNumber, lastRequestDate.Value);

                    //await Console.Out.WriteAsync($"Getting files for request {request.trackingNumber}... ");

                    //var files = await _client.GetRequestFiles(request.trackingNumber);

                    //await Console.Out.WriteLineAsync($"Got {files.data.Length}");

                    //if (files.data.Any())
                    //{

                    //}
                }

                if (offset >= requests.recordsTotal)
                {
                    keepGoingPages = false;
                }

                offset = offset + requests.data.Length;

                Thread.Sleep(1000);
            } while (keepGoingPages);

            if (lastRequestDate >= DateTime.Now)
            {
                keepGoing = false;
            }

            lastRequestDate = lastRequestDate.Value.AddDays(7);

        } while (keepGoing);

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}