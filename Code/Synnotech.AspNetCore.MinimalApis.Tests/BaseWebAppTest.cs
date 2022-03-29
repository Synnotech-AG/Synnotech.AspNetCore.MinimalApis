using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public abstract class BaseWebAppTest : IAsyncLifetime
{
    protected BaseWebAppTest(ITestOutputHelper output)
    {
        Output = output;

        ILogger logger = output.CreateTestLogger();

        var builder = WebApplication.CreateBuilder();
        Log.Logger = logger;
        builder.Host.UseSerilog(logger);
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationServiceStub>();
        builder.Services.AddSingleton<IWebHostEnvironment, WebHostEnvironmentStub>();

        App = builder.Build();
        App.Urls.Add(TestServerSettings.GetHostUrlWithPort());
        App.AddStatusCodeResponses()
           .AddObjectResponses()
           .AddRedirectAndNotAllowedResponses()
           .AddFileResponses();
    }

    protected static string Url { get; set; } = TestServerSettings.GetHostUrlWithPort();

    protected ITestOutputHelper Output { get; }

    private WebApplication App { get; }

    protected HttpClient HttpClient { get; } =
        new () { BaseAddress = new Uri(Url, UriKind.Absolute) };

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