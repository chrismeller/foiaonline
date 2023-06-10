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
            lastRequestDate = new DateTime(2005, 1, 1);
        }


        var keepGoing = true;
        do
        {
            var fromDate = lastRequestDate.Value;
            var toDate = lastRequestDate.Value.AddMonths(1).AddDays(-1);

            var offset = 0;
            var keepGoingPages = true;
            do
            {
                _logger.LogInformation($"Getting requests for {fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd} at offset {offset}");

                var requests =
                    await _client.GetSearchResult(fromDate, toDate, offset);

                _logger.LogInformation($"Got {requests.data.Length} requests");
                _logger.LogInformation($"There are {requests.recordsTotal} total records.");

                var requestsToSave = requests.data.ToDictionary(x => x.trackingNumber, x => lastRequestDate.Value);

                await _service.LogFoundRequests(requestsToSave);

                _logger.LogInformation($"Saved {requestsToSave.Count} requests");

                //foreach (var request in requests.data)
                //{
                //    _logger.LogDebug($"Logging request {request.trackingNumber} as found.");

                //    await _service.LogFoundRequest(request.trackingNumber, lastRequestDate.Value);

                //    //await Console.Out.WriteAsync($"Getting files for request {request.trackingNumber}... ");

                //    //var files = await _client.GetRequestFiles(request.trackingNumber);

                //    //await Console.Out.WriteLineAsync($"Got {files.data.Length}");

                //    //if (files.data.Any())
                //    //{

                //    //}
                //}

                offset = offset + requests.data.Length;

                if (offset >= requests.recordsTotal)
                {
                    keepGoingPages = false;
                }

                

                Thread.Sleep(1000);
            } while (keepGoingPages);

            if (lastRequestDate >= DateTime.Now)
            {
                keepGoing = false;
            }

            lastRequestDate = toDate.AddDays(1);

        } while (keepGoing);

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}