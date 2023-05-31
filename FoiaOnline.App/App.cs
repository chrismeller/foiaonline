using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;

namespace FoiaOnline.App;

public class App : IHostedService
{
    async Task<TokenCookie> GetCsrfToken()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://foiaonline.gov/foiaonline/action/public/search/advancedSearch");

        var content = await response.Content.ReadAsStringAsync();

        var token = Regex.Match(content, "token: \"([\\w\\-]+)\"").Groups[1].Value;

        var cookies = response.Headers.GetValues("Set-Cookie");
        var cookieStrings = cookies.Select(c => c.Split(';')[0]);
        var cookie = string.Join("; ", cookieStrings);

        return new TokenCookie
        {
            Cookie = cookie,
            Token = token,
        };
    }

    async Task GetSearchResult(string token, string cookie, DateTime startDate, DateTime endDate, int offset = 0)
    {
        using var http = new HttpClient();
        var json = JsonSerializer.Serialize(new SearchRequest
        {
            draw = 1,
            numberOfRecords = 25,
            lastItemDisplayed = offset,
            toOrganizationIncludeSubAgencies = true,
            receivedDateFrom = startDate.ToString("yyyy-MM-dd"),
            receivedDateTo = endDate.ToString("yyyy-MM-dd"),
        });
        var request = new HttpRequestMessage
        {
            Headers =
        {
            { "x-csrf-token", token },
            { "referer", "https://foiaonline.gov/foiaonline/action/public/search/advancedSearch" },
            { "x-foia-page-url", "https://foiaonline.gov/foiaonline/action/public/search/advancedSearch" },
            { "cookie", cookie },
            { "x-requested-with", "XMLHttpRequest" },
            { "origin", "https://foiaonline.gov" },
            { "accept", "application/json" },
        },
            Content = new StringContent(json)
            {
                Headers = { ContentType = MediaTypeHeaderValue.Parse("application/json"), }
            },
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://foiaonline.gov/foiaonline/api/search/advancedSearch"),
        };
        var response = await http.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();
        var content = await response.Content.ReadFromJsonAsync<SearchResponse>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        

        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


public class TokenCookie
{
    public string Token { get; set; } = null!;
    public string Cookie { get; set; } = null!;
}

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



public class SearchResponse
{
    public int draw { get; set; }
    public int recordsTotal { get; set; }
    public int recordsFiltered { get; set; }
    public Datum[] data { get; set; }
    public object dataMap { get; set; }
}

public class Datum
{
    public string additionalDisposition { get; set; }
    public string agency { get; set; }
    public string agencyAcronym { get; set; }
    public string agencyOrgChain { get; set; }
    public int clockDays { get; set; }
    public DateTime closedDate { get; set; }
    public string description { get; set; }
    public object descriptionFilter { get; set; }
    public object descriptionModified { get; set; }
    public DateTime due { get; set; }
    public string exemptionsUsed { get; set; }
    public string finalDisposition { get; set; }
    public string id { get; set; }
    public object originalTrackingNumber { get; set; }
    public object otherDisposition { get; set; }
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
    public string dispositionsUsed { get; set; }
    public bool userCaseFileRequester { get; set; }
    public string _score { get; set; }
}
