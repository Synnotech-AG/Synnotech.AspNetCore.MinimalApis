namespace Synnotech.AspNetCore.MinimalApis.Tests;

public sealed class Location
{
    public static Location Default { get; } = new () { Url = "http://test.Location.Default.Url" };

    public string Url { get; init; } = string.Empty;
}