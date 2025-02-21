using PactNet.Verifier;
using Xunit.Abstractions;

namespace Provider.Tests;

public abstract class BaseTests : IDisposable
{
    private string _branch;
    private string _commitSha;

    public BaseTests(ITestOutputHelper output)
    {
        //TODO
        PactVerifier = new TestSetup().SetupPact(output);
        PactBrokerBaseUrl = Environment.GetEnvironmentVariable("PACT_BROKER_BASE_URL");
        PactBrokerToken = Environment.GetEnvironmentVariable("PACT_BROKER_TOKEN");
        Branch = Environment.GetEnvironmentVariable("GITHUB_REF_NAME");
        CommitSha = Environment.GetEnvironmentVariable("GITHUB_SHA");
        ProviderUrl = new Uri(Environment.GetEnvironmentVariable("PROVIDER_URL"));
        ConsumerName = Environment.GetEnvironmentVariable("CONSUMER_NAME");
        ProviderName = Environment.GetEnvironmentVariable("PROVIDER_NAME");
    }

    protected string PactBrokerBaseUrl { get; }
    protected string PactBrokerToken { get; }
    protected PactVerifier PactVerifier { get; }
    protected Uri ProviderUrl { get; }
    protected string ConsumerName { get; }
    protected string ProviderName { get; }

    protected string Branch
    {
        get => string.IsNullOrEmpty(_branch) ? "feature/local" : _branch;
        set => _branch = value;
    }

    protected string CommitSha
    {
        get => string.IsNullOrEmpty(_commitSha) ? Guid.NewGuid().ToString() : _commitSha;
        set => _commitSha = value;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PactVerifier.Dispose();
    }
}