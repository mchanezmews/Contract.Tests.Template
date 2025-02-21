using System.Text;

namespace Consumer.Tests;

public class Sut(HttpClient client)
{
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return await client.SendAsync(new HttpRequestMessage
        {
            Headers = { { "Accept", "application/json" } },
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestUri, UriKind.Relative)
        });
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T payload)
    {
        return await client.SendAsync(new HttpRequestMessage
        {
            Headers = { { "Accept", "application/json" } },
            Method = HttpMethod.Post,
            RequestUri = new Uri(requestUri, UriKind.Relative),
            Content = new StringContent(Serializer.Serialize(payload), Encoding.UTF8, "application/json")
        });
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string requestUri, T payload)
    {
        return await client.SendAsync(new HttpRequestMessage
        {
            Headers = { { "Accept", "application/json" } },
            Method = HttpMethod.Put,
            RequestUri = new Uri(requestUri, UriKind.Relative),
            Content = new StringContent(Serializer.Serialize(payload), Encoding.UTF8, "application/json")
        });
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return await client.SendAsync(new HttpRequestMessage
        {
            Headers = { { "Accept", "application/json" } },
            Method = HttpMethod.Delete,
            RequestUri = new Uri(requestUri, UriKind.Relative)
        });
    }
}