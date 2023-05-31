namespace FoiaOnline.Data.Models;

public class FoundRequest
{
    public string TrackingNumber { get; set; } = string.Empty;
    public bool IsScraped { get; set; } = false;
}