using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseSuccessStatusTests : BaseWebAppTest
{
    public HttpResponseSuccessStatusTests(ITestOutputHelper output) : base(output) { }

    // Status Code 200 OK
    [Fact]
    public async Task OkWithoutBodyTest()
    {
        var response = await HttpClient.GetAsync("/api/ok");
    
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task OkWithBodyTest()
    {
        // act
        var response = await HttpClient.GetAsync("/api/ok/body");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
    
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        value.Should().BeEquivalentTo(Contact.Default);
    }
    
    
    // Status Code 201 Created
    [Fact]
    public async Task CreatedWithStringAsLocationTest()
    {
        var response = await HttpClient.GetAsync("/api/created/string");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }
    
    [Fact]
    public async Task CreatedWithUriAsLocationTest()
    {
        var response = await HttpClient.GetAsync("/api/created/uri");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }
    
    // Status Code 202 Accepted
    [Fact]
    public async Task AcceptedWithoutLocationProvidedTest()
    {
        var response = await HttpClient.GetAsync("/api/accepted");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
    
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        value.Should().BeEquivalentTo(Contact.Default);
    }
    
    [Fact]
    public async Task AcceptedWithStringAsLocationTest()
    {
        var response = await HttpClient.GetAsync("/api/accepted/string");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }
    
    [Fact]
    public async Task AcceptedWithUriAsLocationTest()
    {
        var response = await HttpClient.GetAsync("/api/accepted/uri");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }
    
    
    // Status Code 204 No Content
    [Fact]
    public async Task NoContentTest()
    {
        // act
        var response = await HttpClient.GetAsync("/api/noContent");
    
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    private Uri? GetUriFromHttpResponseMessage(HttpResponseMessage response)
    {
        return response.Headers.Location;
    }
}