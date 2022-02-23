using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Xunit;
using Xunit.Abstractions;
using SynnotechTestSettings = Synnotech.Xunit.TestSettings;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public abstract class BaseWebAppTest : IAsyncLifetime
{
    protected BaseWebAppTest(ITestOutputHelper output)
    {
        TestServerHostUrl = TestServerSettings.GetHostUrlWithPort();
        Output = output;

        App = WebApplication.Create();
        App.Urls.Add(TestServerHostUrl);
        App.AddStatusCodeResponses()
           .AddObjectResponses()
           .AddRedirectAndForbiddenResponses()
           .AddFileResponses();
    }

    protected static string? TestServerHostUrl { get; set; }

    protected ITestOutputHelper Output { get; }

    private WebApplication App { get; }

    protected HttpClient HttpClient { get; } =
        new () { BaseAddress = new Uri(TestServerHostUrl!, UriKind.Absolute) };

    public Task InitializeAsync() => App.StartAsync();

    public async Task DisposeAsync()
    {
        try
        {
            await App.StopAsync();
            await App.DisposeAsync();
        }
        catch (Exception exception)
        {
            Output.WriteLine("Exception occurred while stopping the web app");
            Output.WriteLine(exception.ToString());
        }

        try
        {
            HttpClient.Dispose();
        }
        catch (Exception exception)
        {
            Output.WriteLine("Exception occurred while disposing the HTTP client");
            Output.WriteLine(exception.ToString());
        }
    }
}

public static class TestServerSettings
{
    public static string GetHostUrlWithPort() =>
        SynnotechTestSettings.Configuration["TestServer:hostUrlWithPort"];
}