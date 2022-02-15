using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public abstract class HttpResponseTestsBase
{
    protected readonly HttpClient Client;

    // test values that only are used for current test environment
    protected const string Value = "Test";
    protected static readonly Uri Location = new("http://test.url");

    protected HttpResponseTestsBase()
    {
        // start in-Memory testServer for simple integration testing
        var app = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        Client = app.CreateClient();
    }

    protected Uri? GetUriFromHttpResponseMessage(HttpResponseMessage response)
    {
        return response.Headers.Location;
    }

    protected async Task<string> GetAndFormatStringContentFromHttpResponseMessage(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();

        // response strings always have quotation marks at the start and end
        // for cleaner testing they get trimmed in this method
        responseString = responseString.Trim('\"');

        return responseString;
    }

    protected async Task<HttpResponseMessage> GetHttpResponseMessageFromApi(string url)
    {
        return await Client.GetAsync("/api" + url);
    }
}