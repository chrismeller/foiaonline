namespace FoiaOnline.Client.DTOs;
public class SearchResponse
{
    public int draw { get; set; }
    public int recordsTotal { get; set; }
    public int recordsFiltered { get; set; }
    public SearchResponseResult[] data { get; set; }
    public object dataMap { get; set; }
}

public class SearchResponseResult
{
    public string additionalDisposition { get; set; }
    public string agency { get; set; }
    public string agencyAcronym { get; set; }
    public string agencyOrgChain { get; set; }
    public int clockDays { get; set; }
    public DateTime? closedDate { get; set; }
    public string description { get; set; }
    public object descriptionFilter { get; set; }
    public object descriptionModified { get; set; }
    public DateTime? due { get; set; }
    public string exemptionsUsed { get; set; }
    public string finalDisposition { get; set; }
    public string id { get; set; }
    public object originalTrackingNumber { get; set; }
    public string otherDisposition { get; set; }
    public string parentType { get; set; }
    public string phase { get; set; }
    public bool privateFlag { get; set; }
    public DateTime received { get; set; }
    public object recordAuthor { get; set; }
    public object recordExemptionsUsed { get; set; }
    public object recordFileFormat { get; set; }
    public object recordId { get; set; }
    public object recordName { get; set; }
    public object recordReleaseDate { get; set; }
    public object recordRetentionPeriod { get; set; }
    public int recordSize { get; set; }
    public string reportingYear { get; set; }
    public string requestType { get; set; }
    public string requester { get; set; }
    public string requesterOrg { get; set; }
    public string trackingNumber { get; set; }
    public string type { get; set; }
    public bool userCaseFileRequester { get; set; }
    public string dispositionsUsed { get; set; }
    public string _score { get; set; }
}
