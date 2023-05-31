namespace FoiaOnline.Client.DTOs;

public class RequestFilesRequest
{
    public int numberOfRecords { get; set; }
    public int lastItemDisplayed { get; set; }
    public int draw { get; set; }
}
