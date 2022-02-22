using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public abstract class BaseWebAppTest : IAsyncLifetime
{
    private const string Url = "http://localhost:5000/";
    
    protected BaseWebAppTest(ITestOutputHelper output)
    {
        Output = output;
        
        App = WebApplication.Create();
        App.Urls.Add(Url);
        App.AddStatusCodeResponses()
           .AddObjectResponses()
           .AddRedirectAndForbiddenResponses()
           .AddFileResponses();
    }

    protected ITestOutputHelper Output { get; }

    private WebApplication App { get; }

    protected HttpClient HttpClient { get; } =
        new () { BaseAddress = new Uri(Url, UriKind.Absolute)};
    
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