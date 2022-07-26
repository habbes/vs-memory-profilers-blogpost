// See https://aka.ms/new-console-template for more information

HttpClient httpClient = new();
string[] endpoints = new string[]
{
    "/stats/Nairobi/Clothing",
    "/stats/Redmond/Electronics",
    "/stats/Oslo/Cosmetics"
};

int numRounds = 100;
int requestsPerRound = 100;
string baseUrl = "http://localhost:5108";

Console.WriteLine($"Sending {numRounds * requestsPerRound} requests...");
await ExecuteRequests(baseUrl, endpoints, numRounds, requestsPerRound);
Console.WriteLine("Done!");

async Task ExecuteRequests(string baseUrl, string[] endpoints, int numRounds, int requestsPerRound)
{
    for (int i = 0; i < numRounds; i++)
    {
        var urls = GenerateRequestUrls(baseUrl, endpoints, requestsPerRound);
        await ExecuteConcurrentRequests(urls);
        await Task.Delay(10); // brief pause between rounds
    }
}

async Task ExecuteConcurrentRequests(IEnumerable<string> urls)
{
    await Task.WhenAll(urls.Select(url => MakeRequest(httpClient, url)));
}

async Task MakeRequest(HttpClient httpClient, string url)
{
    var response = await httpClient.GetAsync(url);
    var resp = await response.Content.ReadAsStringAsync();
}

IEnumerable<string> GenerateRequestUrls(string baseUrl, string[] endpoints, int count)
{
    for (int i = 0; i < count; i++)
    {
        int endpointIndex = i % endpoints.Length;
        string endpoint = endpoints[endpointIndex];
        yield return $"{baseUrl}{endpoint}";
    }
}
