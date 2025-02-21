using System.Net;
using Consumer.Tests.Data;
using Consumer.Tests.Data.Model;
using PactNet.Matchers;
using Xunit.Abstractions;

namespace Consumer.Tests;

public class ExampleConsumerTests : BaseTests, IClassFixture<TestFixture>
{
    private readonly ExampleData _exampleData;

    public ExampleConsumerTests(ITestOutputHelper output) : base(output)
    {
        //TODO
        _exampleData = new ExampleData();
    }

    [Fact]
    public async Task GET_Example()
    {
        //Arrange
        var endpoint = "/api/country";
        var country = _exampleData.Country;

        PactBuilder
            .UponReceiving($"A GET {endpoint} request")
            .Given("The GET request is valid")
            .WithRequest(HttpMethod.Get, endpoint)
            .WithQuery("code", country.Code)
            .WithHeader("Accept", "application/json")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(Match.Type(country));

        //Act 
        await PactBuilder.VerifyAsync(async ctx =>
        {
            var sut = new Sut(HttpClientFactory.CreateClient(ctx.MockServerUri.ToString()));
            var response = await sut.GetAsync($"{endpoint}?code={country.Code}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var deserializedCountry = Serializer.Deserialize<ExampleModel>(jsonResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(deserializedCountry);
        });
    }

    [Fact]
    public async Task POST_Example()
    {
        //Arrange
        var endpoint = "/api/country";
        var country = _exampleData.Country;

        PactBuilder
            .UponReceiving($"A POST {endpoint} request")
            .Given("The POST request is valid")
            .WithRequest(HttpMethod.Post, endpoint)
            .WithBody(Serializer.Serialize(country), "application/json")
            .WithHeader("Accept", "application/json")
            .WillRespond()
            .WithStatus(HttpStatusCode.Created)
            .WithJsonBody(Match.Type(country));

        //Act 
        await PactBuilder.VerifyAsync(async ctx =>
        {
            var sut = new Sut(HttpClientFactory.CreateClient(ctx.MockServerUri.ToString()));
            var response = await sut.PostAsync(endpoint, country);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var deserializedCountry = Serializer.Deserialize<ExampleModel>(jsonResponse);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(deserializedCountry);
        });
    }
}