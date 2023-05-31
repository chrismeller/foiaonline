namespace FoiaOnline.Client.DTOs;

public class RequestFilesResponse
{
    public int draw { get; set; }
    public int recordsTotal { get; set; }
    public int recordsFiltered { get; set; }
    public RequestFileData[] data { get; set; }
    public object dataMap { get; set; }
}

public class RequestFileData
{
    public string trackingNumber { get; set; }
    public string title { get; set; }
    public string fileName { get; set; }
    public string releaseType { get; set; }
    public string[] exemptions { get; set; }
    public string[] ex3statutes { get; set; }
    public string[] ex5subtypes { get; set; }
    public object keywords { get; set; }
    public string uploadedBy { get; set; }
    public string uploadedByEmail { get; set; }
    public string createdDate { get; set; }
    public string lastModifiedDate { get; set; }
    public string fileType { get; set; }
    public object fileFormat { get; set; }
    public object author { get; set; }
    public string addedBy { get; set; }
    public string size { get; set; }
    public string recordReleaseDate { get; set; }
    public string retentionPeriod { get; set; }
    public object frequentlyRequested { get; set; }
    public string type { get; set; }
    public string recordId { get; set; }
    public string referredToNonparticipatingAgency { get; set; }
    public string referredFromOtherAgency { get; set; }
    public object referredType { get; set; }
    public string recordType { get; set; }
    public object taskId { get; set; }
    public bool isPlaceholder { get; set; }
}
