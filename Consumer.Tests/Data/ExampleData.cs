using Consumer.Tests.Data.Model;

namespace Consumer.Tests.Data;

public class ExampleData
{
    public ExampleModel Country =>
        new()
        {
            Code = "IE",
            Name = "Ireland"
        };
}