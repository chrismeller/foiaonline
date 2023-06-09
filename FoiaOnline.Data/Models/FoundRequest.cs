namespace FoiaOnline.Data.Models;

public class FoundRequest
{
    public string TrackingNumber { get; set; } = string.Empty;
    public DateTime SearchDate { get; set; }
    public bool IsScraped { get; set; } = false;
}