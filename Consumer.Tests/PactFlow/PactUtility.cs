using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Consumer.Tests;

public class PactUtility
{
    private readonly HttpClient _httpClient;

    public PactUtility(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> PublishPactContract
    (
        string pactBrokerBaseUrl,
        string pactBrokerToken,
        string consumerName,
        string providerName,
        string contractContent,
        string branch,
        string commitSha
    )
    {
        var endpoint = "/contracts/publish";
        var contract = new Contract
        {
            ConsumerName = consumerName,
            ProviderName = providerName,
            Content = contractContent
        };
        var contractsPublish = new ContractsPublish
        {
            PacticipantName = consumerName,
            PacticipantVersionNumber = commitSha,
            Branch = branch,
            Contracts = [contract]
        };
        var serializedContent = Serializer.Serialize(contractsPublish, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var jsonAsStringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
        _httpClient.BaseAddress = new Uri(pactBrokerBaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", pactBrokerToken);
        return await _httpClient.PostAsync(endpoint, jsonAsStringContent);
    }
}