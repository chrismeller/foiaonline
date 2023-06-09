using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using FoiaOnline.Client.DTOs;

namespace FoiaOnline.Client;

public class FoiaClient
{
    private string? _token;
    private string? _cookie;

    async Task GetCsrfToken()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://foiaonline.gov/foiaonline/action/public/search/advancedSearch");

        var content = await response.Content.ReadAsStringAsync();

        var token = Regex.Match(content, "token: \"([\\w\\-]+)\"").Groups[1].Value;

        var cookies = response.Headers.GetValues("Set-Cookie");
        var cookieStrings = cookies.Select(c => c.Split(';')[0]);
        var cookie = string.Join("; ", cookieStrings);

        _token = token;
        _cookie = cookie;
    }

    public async Task<SearchResponse> GetSearchResult(DateTime startDate, DateTime endDate, int offset = 0)
    {
        if (_token == null) await GetCsrfToken();

        using var http = new HttpClient();
        var json = JsonSerializer.Serialize(new SearchRequest
        {
            draw = 1,
            numberOfRecords = 1000,
            lastItemDisplayed = offset,
            toOrganizationIncludeSubAgencies = true,
            receivedDateFrom = startDate.ToString("yyyy-MM-dd"),
            receivedDateTo = endDate.ToString("yyyy-MM-dd"),
        });
        var request = new HttpRequestMessage
        {
            Headers =
        {
            { "x-csrf-token", _token },
            { "referer", "https://foiaonline.gov/foiaonline/action/public/search/advancedSearch" },
            { "x-foia-page-url", "https://foiaonline.gov/foiaonline/action/public/search/advancedSearch" },
            { "cookie", _cookie },
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

        return content;
    }

    public async Task<RequestFilesResponse> GetRequestFiles(string trackingNumber)
    {
        if (_token == null) await GetCsrfToken();

        using var http = new HttpClient();
        var json = JsonSerializer.Serialize(new RequestFilesRequest
        {
            draw = 1,
            lastItemDisplayed = 0,
            numberOfRecords = 1000,
        });
        var request = new HttpRequestMessage
        {
            Headers =
            {
                { "x-csrf-token", _token },
                { "referer", $"https://foiaonline.gov/foiaonline/action/public/submissionDetails?trackingNumber={trackingNumber}&type=Request" },
                { "x-foia-page-url", $"https://foiaonline.gov/foiaonline/action/public/submissionDetails?trackingNumber={trackingNumber}&type=Request" },
                { "cookie", _cookie },
                { "x-requested-with", "XMLHttpRequest" },
                { "origin", "https://foiaonline.gov" },
                { "accept", "application/json" },
            },
            Content = new StringContent(json)
            {
                Headers = { ContentType = MediaTypeHeaderValue.Parse("application/json"), }
            },
            Method = HttpMethod.Post,
            RequestUri = new Uri($"https://foiaonline.gov/foiaonline/api/request/publicRecords/{trackingNumber}/Request"),
        };
        var response = await http.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();
        var content = await response.Content.ReadFromJsonAsync<RequestFilesResponse>();

        return content;
    }
}