using Microsoft.Extensions.Configuration;
using Synnotech.Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class TestServerSettings
{
    public static string GetHostUrlWithPort()
    {
        var url = TestSettings.Configuration.GetValue<string>("testServer:hostUrlWithPort");

        // TODO: just a temporary fix because it's currently not working
        if (string.IsNullOrEmpty(url))
        {
            url = "http://localhost:5000/";
        }

        return url;
    }
}