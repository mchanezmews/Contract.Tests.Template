using PactNet;
using Xunit.Abstractions;

namespace Consumer.Tests;

public abstract class BaseTests
{
    public BaseTests(ITestOutputHelper output)
    {
        //TODO
        HttpClientFactory = new HttpClientFactory();
        PactBuilder = new TestSetup().SetupPact(output);
    }

    protected IPactBuilderV4 PactBuilder { get; }
    protected HttpClientFactory HttpClientFactory { get; }
}