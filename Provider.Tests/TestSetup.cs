using System.Runtime.CompilerServices;
using DotNetEnv;
using PactNet;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace Provider.Tests;

public class TestSetup
{
    public PactVerifier SetupPact(ITestOutputHelper output)
    {
        IsLocal();
        var config = new PactVerifierConfig
        {
            Outputters = new[]
            {
                new XunitOutput(output)
            },
            LogLevel = PactLogLevel.Debug
        };
        return new PactVerifier(config);
    }
    
    private void IsLocal()
    {
        if (Environment.GetEnvironmentVariable("IS_LOCAL") is null)
        {
            var classPath = Path.GetDirectoryName(GetPath()) + "/";
            Env.Load(classPath);
        }
    }

    private string GetPath([CallerFilePath] string fileName = null)
    {
        return fileName;
    }
}