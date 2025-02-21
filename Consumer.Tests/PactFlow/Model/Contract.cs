namespace Consumer.Tests;

public class Contract
{
    public string ConsumerName { get; set; }
    public string ProviderName { get; set; }
    public string Content { get; set; }
    public string ContentType => "application/json";
    public string Specification => "pact";
}