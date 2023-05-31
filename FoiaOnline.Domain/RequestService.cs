using FoiaOnline.Data;
using FoiaOnline.Data.Models;

namespace FoiaOnline.Domain;

public class RequestService
{
    private readonly ApplicationDbContext _dbContext;

    public RequestService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogFoundRequest(string trackingNumber)
    {
        await _dbContext.FoundRequests.AddAsync(new FoundRequest
        {
            TrackingNumber = trackingNumber,
        });

        await _dbContext.SaveChangesAsync();
    }
}