using System.Text.Json;

namespace Consumer.Tests;

public class Serializer
{
    public static string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(value, options);
    }

    public static TValue? Deserialize<TValue>(string value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<TValue>(value, options);
    }
}