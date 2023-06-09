using FoiaOnline.Data;
using FoiaOnline.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoiaOnline.Domain;

public class RequestService
{
    private readonly ApplicationDbContext _dbContext;

    public RequestService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogFoundRequest(string trackingNumber, DateTime searchDate)
    {
        if (await _dbContext.FoundRequests.AnyAsync(x => x.TrackingNumber == trackingNumber)) return;

        await _dbContext.FoundRequests.AddAsync(new FoundRequest
        {
            TrackingNumber = trackingNumber,
            IsScraped = false,
            SearchDate = searchDate,
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task LogFoundRequests(Dictionary<string, DateTime> foundRequests)
    {
        //var existing =
        //    await _dbContext.FoundRequests.Where(x => foundRequests.Keys.Contains(x.TrackingNumber))
        //        .ToListAsync();

        //var toCreate = foundRequests.Where(x => existing.Select(e => e.TrackingNumber).Contains(x.Key) == false)
        //    .Select(x => new FoundRequest());

        //await _dbContext.FoundRequests.AddRangeAsync()
        await _dbContext.Database.BeginTransactionAsync();

        foreach (var request in foundRequests)
        {
            await LogFoundRequest(request.Key, request.Value);
        }

        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task<DateTime?> GetLastFoundRequestDate()
    {
        var last = await _dbContext.FoundRequests.OrderByDescending(x => x.SearchDate).FirstOrDefaultAsync();

        return last?.SearchDate;
    }

    public async Task MarkRequestScraped(string trackingNumber)
    {
        var request = await _dbContext.FoundRequests.FirstOrDefaultAsync(x => x.TrackingNumber == trackingNumber);

        if (request == null) return;

        request.IsScraped = true;

        await _dbContext.SaveChangesAsync();
    }
}