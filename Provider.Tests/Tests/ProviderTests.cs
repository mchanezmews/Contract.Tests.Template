using Xunit.Abstractions;

namespace Provider.Tests;

public class ProviderTests(ITestOutputHelper output) : BaseTests(output)
{
    [Fact]
    public void Check_Provider_Honours_Pact_With_Consumer()
    {
        // Arrange // Act // Assert 
        PactVerifier
            .ServiceProvider(ProviderName, ProviderUrl)
            .WithPactBrokerSource(new Uri(PactBrokerBaseUrl), configure =>
            {
                configure.TokenAuthentication(PactBrokerToken);
                configure.ProviderBranch(Branch);
                configure.PublishResults(true, CommitSha);
            })
            .Verify();
    }

    [Fact(Skip = "Enable only if testing Pacts locally without Pactflow.io")]
    public void Check_Provider_Honours_Pact_With_Consumer_Locally()
    {
        // Arrange
        var localPactName = $"{ConsumerName}-{ProviderName}";
        var localPathFolder = Environment.GetEnvironmentVariable("LOCAL_PACT_FOLDER");
        var localPactFile = new FileInfo($"{localPathFolder}/{localPactName}.json");

        // Act // Assert 
        PactVerifier
            .ServiceProvider(ProviderName, ProviderUrl)
            .WithFileSource(localPactFile)
            .Verify();
    }
}