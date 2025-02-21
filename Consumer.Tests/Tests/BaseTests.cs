using PactNet;
using Xunit.Abstractions;

namespace Consumer.Tests;

public abstract class BaseTests : IClassFixture<TestFixture>
{
    public BaseTests(ITestOutputHelper output)
    {
        HttpClientFactory = new HttpClientFactory();
        PactBuilder = new TestSetup().SetupPact(output);
    }

    protected IPactBuilderV4 PactBuilder { get; }
    protected HttpClientFactory HttpClientFactory { get; }
}