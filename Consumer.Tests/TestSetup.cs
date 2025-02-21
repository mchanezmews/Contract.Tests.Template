using PactNet;
using PactNet.Output.Xunit;
using Xunit.Abstractions;

namespace Consumer.Tests;

public class TestSetup
{
    public IPactBuilderV4 SetupPact(ITestOutputHelper output)
    {
        var localPathFolder = Environment.GetEnvironmentVariable("LOCAL_PACT_FOLDER");
        var config = new PactConfig
        {
            PactDir = Path.Combine(Directory.GetCurrentDirectory(), localPathFolder),
            Outputters = new[]
            {
                new XunitOutput(output)
            },
            LogLevel = PactLogLevel.Debug
        };
        var consumer = Environment.GetEnvironmentVariable("CONSUMER_NAME");
        var provider = Environment.GetEnvironmentVariable("PROVIDER_NAME");
        var pact = Pact.V4(consumer, provider, config);
        return pact.WithHttpInteractions();
    }
}