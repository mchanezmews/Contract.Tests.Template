namespace Consumer.Tests;

public class HttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string url)
    {
        return new HttpClient
        {
            BaseAddress = new Uri(url)
        };
    }
}