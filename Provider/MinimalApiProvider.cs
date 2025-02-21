using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;

//Note: All classes are in the same file for example/demo purposes
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICountryService, CountryService>();
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.PropertyNamingPolicy = null; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "Hello, Mews!");

// Example 1: Get all countries
app.MapGet("/api/countries", (ICountryService countryService) =>
        TypedResults.Ok(countryService.GetCountries()))
    .WithName("GetCountries");

// Example 2: Get a specific country by code
app.MapGet("/api/country", Results<Ok<Country>, NotFound> (ICountryService countryService, string code) =>
{
    var country = countryService.GetCountry(code);
    return TypedResults.Ok(country);
}).WithName("GetCountryByCode");

// Example 3: Add a new country
app.MapPost("/api/country", (ICountryService countryService, Country country) =>
{
    countryService.AddCountry(country);
    return TypedResults.Created($"/country?{country.Code}", country);
}).WithName("AddCountry");

app.Run();

public interface ICountryService
{
    List<Country> GetCountries();

    Country GetCountry(string code);

    void AddCountry(Country country);
}

public class CountryService : ICountryService
{
    private readonly List<Country> _countries;

    public CountryService()
    {
        _countries = new List<Country>
        {
            new()
            {
                Code = "IE",
                Name = "Ireland"
            },
            new()
            {
                Code = "US",
                Name = "United States"
            },
            new()
            {
                Code = "CA",
                Name = "Canada"
            }
        };
    }

    public List<Country> GetCountries()
    {
        return _countries;
    }

    public Country GetCountry(string code)
    {
        return _countries.FirstOrDefault(x => x.Code == code);
    }

    public void AddCountry(Country country)
    {
        _countries.Add(country);
    }
}

public class Country
{
    public string Code { get; set; }
    public string Name { get; set; }
}