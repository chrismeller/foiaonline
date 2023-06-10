using EFCore.BulkExtensions;
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
        var entities = foundRequests.Select(x => new FoundRequest
        {
            TrackingNumber = x.Key,
            SearchDate = x.Value,
            IsScraped = false,
        });

        await _dbContext.BulkInsertOrUpdateAsync(entities);
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