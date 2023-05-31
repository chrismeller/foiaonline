namespace FoiaOnline.App.DTOs;


public class SearchRequest
{
    public object[] appealDisposition { get; set; }
    public object[] customField { get; set; }
    public object[] exemptions { get; set; }
    public object[] phase { get; set; }
    public object[] processTime { get; set; }
    public object[] requestDisposition { get; set; }
    public object[] requestTypes { get; set; }
    public object[] status { get; set; }
    public object[] statutes { get; set; }
    public object[] subtypes { get; set; }
    public object[] taskType { get; set; }
    public object[] toOrganization { get; set; }
    public object[] toIndividual { get; set; }
    public object[] track { get; set; }
    public object[] type { get; set; }
    public int draw { get; set; }
    public int lastItemDisplayed { get; set; }
    public int numberOfRecords { get; set; }
    public bool toOrganizationIncludeSubAgencies { get; set; }
    public string receivedDateFrom { get; set; }
    public string receivedDateTo { get; set; }
}
