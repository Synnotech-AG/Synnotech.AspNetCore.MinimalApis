using Microsoft.AspNetCore.Mvc.Routing;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public sealed class Location
{
    public static Location Default { get; } = new () { Url = "http://test.Location.Default.Url" };

    public static Location DefaultRedirect { get; } = new () { Url = TestServerSettings.GetHostUrlWithPort() + "api/ok" };

    public string Url { get; init; } = string.Empty;
}