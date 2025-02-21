using System.Net;
using System.Runtime.CompilerServices;
using DotNetEnv;

namespace Consumer.Tests;

public class TestFixture : IDisposable
{
    private readonly string _branch;
    private readonly string _commitSha;
    private readonly string _consumerName;
    private readonly string _pactBrokerBaseUrl;
    private readonly string _pactBrokerToken;
    private readonly string _providerName;

    public TestFixture()
    {
        IsLocal();
        _pactBrokerBaseUrl = Environment.GetEnvironmentVariable("PACT_BROKER_BASE_URL");
        _pactBrokerToken = Environment.GetEnvironmentVariable("PACT_BROKER_TOKEN");
        var branch = Environment.GetEnvironmentVariable("GITHUB_REF_NAME");
        _branch = string.IsNullOrEmpty(branch) ? "feature/local" : branch;
        var commitSha = Environment.GetEnvironmentVariable("GITHUB_SHA");
        _commitSha = string.IsNullOrEmpty(commitSha) ? Guid.NewGuid().ToString() : commitSha;
        _consumerName = Environment.GetEnvironmentVariable("CONSUMER_NAME");
        _providerName = Environment.GetEnvironmentVariable("PROVIDER_NAME");
    }

    public void Dispose()
    {
        PublishContract().GetAwaiter().GetResult();
    }

    private void IsLocal()
    {
        if (Environment.GetEnvironmentVariable("IS_LOCAL") is null)
        {
            var classPath = Path.GetDirectoryName(GetPath()) + "/";
            Env.Load(classPath);
        }
    }

    private string GetPath([CallerFilePath] string fileName = null)
    {
        return fileName;
    }

    private async Task PublishContract()
    {
        var pactPath =
            $"{Environment.GetEnvironmentVariable("LOCAL_PACT_FOLDER")}/{_consumerName}-{_providerName}.json";
        var contractBytes = await File.ReadAllBytesAsync(pactPath);
        var response = await new PactUtility(new HttpClient()).PublishPactContract
        (
            _pactBrokerBaseUrl,
            _pactBrokerToken,
            _consumerName,
            _providerName,
            Convert.ToBase64String(contractBytes),
            _branch,
            _commitSha
        );
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}