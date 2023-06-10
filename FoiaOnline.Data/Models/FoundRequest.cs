namespace FoiaOnline.Data.Models;

public class FoundRequest
{
    public string TrackingNumber { get; set; } = string.Empty;
    public DateTime SearchDate { get; set; }
    public DateTime ReceiveDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public string Agency { get; set; }
    public string Description { get; set; }
    public string ExemptionsUsed { get; set; }
    public string ReportingYear { get; set; }
    public string Requester { get; set; }
    public string FinalDisposition { get; set; }


    public bool IsScraped { get; set; } = false;
}