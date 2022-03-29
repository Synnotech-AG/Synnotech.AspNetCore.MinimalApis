using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;
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
        using var response = await HttpClient.GetAsync("/api/ok");
    
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task OkWithBodyTest()
    {
        // act
        using var response = await HttpClient.GetAsync("/api/ok/body");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
    
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        value.Should().BeEquivalentTo(Contact.Default);
    }
    
    
    // Status Code 201 Created
    [Fact]
    public async Task Created()
    {
        using var response = await HttpClient.GetAsync("/api/created");

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        await response.ShouldHaveNoContentAsync();
    }

    [Fact]
    public async Task CreatedWithUrlString()
    {
        using var response = await HttpClient.GetAsync("/api/created/string");

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().Be(Location.Default.Url);
        await response.ShouldHaveNoContentAsync();
    }

    [Fact]
    public async Task CreatedWithUri()
    {
        using var response = await HttpClient.GetAsync("/api/created/uri");

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().Be(Location.Default.Url);
        await response.ShouldHaveNoContentAsync();
    }

    [Fact]
    public async Task CreatedWithStringAsLocationTest()
    {
        using var response = await HttpClient.GetAsync("/api/created/withBody/string");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }

    [Fact]
    public async Task CreatedWithBody()
    {
        using var response = await HttpClient.GetAsync("/api/created/withBody");
        var value = await response.Content.ReadFromJsonAsync<Contact>();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        value.Should().BeEquivalentTo(Contact.Default);
    }

    [Fact]
    public async Task CreatedWithUriAsLocationTest()
    {
        using var response = await HttpClient.GetAsync("/api/created/withBody/uri");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }
    
    // Status Code 202 Accepted

    [Fact]
    public async Task Accepted()
    {
        using var response = await HttpClient.GetAsync("/api/accepted");

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task AcceptedWithUrlString()
    {
        using var response = await HttpClient.GetAsync("/api/accepted/string");

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        response.Headers.Location.Should().Be(Location.Default.Url);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task AcceptedWithUri()
    {
        using var response = await HttpClient.GetAsync("/api/accepted/uri");

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        response.Headers.Location.Should().Be(Location.Default.Url);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task AcceptedWithoutLocationProvidedTest()
    {
        using var response = await HttpClient.GetAsync("/api/accepted/withBody");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
    
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        value.Should().BeEquivalentTo(Contact.Default);
    }
    
    [Fact]
    public async Task AcceptedWithStringAsLocationTest()
    {
        using var response = await HttpClient.GetAsync("/api/accepted/withBody/string");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        var responseUrl = GetUriFromHttpResponseMessage(response);
    
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        value.Should().BeEquivalentTo(Contact.Default);
        responseUrl.Should().NotBeNull().And.Be(Location.Default.Url);
    }
    
    [Fact]
    public async Task AcceptedWithUriAsLocationTest()
    {
        using var response = await HttpClient.GetAsync("/api/accepted/withBody/uri");
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
        using var response = await HttpClient.GetAsync("/api/noContent");
    
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    private static Uri? GetUriFromHttpResponseMessage(HttpResponseMessage response)
    {
        return response.Headers.Location;
    }
}