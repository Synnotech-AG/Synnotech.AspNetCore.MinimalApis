using Microsoft.Extensions.Configuration;
using Synnotech.Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class TestServerSettings
{
    public static string GetHostUrlWithPort()
    {
        var url = TestSettings.Configuration.GetValue<string>("testServer:hostUrlWithPort");

        return url;
    }
}