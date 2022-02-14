using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Synnotech.AspNetCore.MinimalApis.Responses;
using Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResondSuccessStatusTest
{
    private readonly HttpClient _client;

    const string Value = "\"Test\"";
    const string UrlString = "test.url";
    private static readonly Uri UrlUri = new Uri(UrlString);

    public HttpResondSuccessStatusTest()
    {
        var app = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {});
        _client = app.CreateClient();
    }

    [Fact]
    public async Task OkWithoutBodyTest()
    {
        // act
        var response = await GetHttpResponseMessageFromApi("/ok");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OkWithBodyTest()
    {
        // act
        var response = await GetHttpResponseMessageFromApi("/ok/body");
        var responseValue = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseValue.Should().Be(Value);
    }

    [Fact]
    public async Task CreatedTest()
    {
        var response = await GetHttpResponseMessageFromApi("/created/string");
        var responseValue = await response.Content.ReadAsStringAsync();
        // var responseUrl = response.Content.Headers.ContentLocation;

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        responseValue.Should().Be(Value);
        // responseUrl.Should().Be(UrlUri);
    }

    private async Task<HttpResponseMessage> GetHttpResponseMessageFromApi(string url)
    {
        return await _client.GetAsync("/api" + url);
    }
}