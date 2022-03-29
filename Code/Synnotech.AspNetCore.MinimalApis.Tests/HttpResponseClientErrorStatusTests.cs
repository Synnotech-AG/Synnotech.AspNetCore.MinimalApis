using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public sealed class HttpResponseClientErrorStatusTests : BaseWebAppTest
{
    public HttpResponseClientErrorStatusTests(ITestOutputHelper output) : base(output) { }

    // Status Code 400 Bad Request
    [Fact]
    public async Task BadRequestWithoutMessageTest()
    {
        using var response = await HttpClient.GetAsync("/api/badRequest");
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task BadRequestWithMessageTest()
    {
        using var response = await HttpClient.GetAsync("/api/badRequest/string");

        var value = await response.Content.ReadFromJsonAsync<Contact>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        value.Should().BeEquivalentTo(Contact.Default);
    }

    // Status Code 401 Unauthorized
    [Fact]
    public async Task UnauthorizedTest()
    {
        using var response = await HttpClient.GetAsync("/api/unauthorized");
    
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrEmpty();

    }

    [Fact]
    public async Task UnauthorizedWithBodyTest()
    {
        using var response = await HttpClient.GetAsync("/api/unauthorized/contact");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        var body = await response.Content.ReadFromJsonAsync<Contact>();
        body.Should().BeEquivalentTo(Contact.Default);
    }
    
    // Status Code 403 Forbidden
    [Fact]
    public async Task Forbidden()
    {
        using var response = await HttpClient.GetAsync("/api/forbidden");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        await response.ShouldHaveNoContentAsync();
    }

    [Fact]
    public async Task ForbiddenWithBody()
    {
        using var response = await HttpClient.GetAsync("/api/forbidden/contact");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        var body = await response.Content.ReadFromJsonAsync<Contact>();
        body.Should().BeEquivalentTo(Contact.Default);
    }

    [Fact]
    public async Task ForbiddenWithoutMessageTest()
    {
        using var response = await HttpClient.GetAsync("/api/notAllowed");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task ForbiddenWithAuthenticationSchemeAsStringTest()
    {
        using var response = await HttpClient.GetAsync("/api/notAllowed/authenticationScheme/string");
        
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task ForbiddenWithAuthenticationPropertiesAndSchemeAsStringTest()
    {
        using var response = await HttpClient.GetAsync("/api/notAllowed/authenticationProperties/string");
    
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ForbiddenWithAuthenticationPropertiesAndSchemeAsListTest()
    {
        using var response = await HttpClient.GetAsync("/api/notAllowed/authenticationProperties/list");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    // Status Code 409 Conflict
    [Fact]
    public async Task ConflictTest()
    {
        using var response = await HttpClient.GetAsync("/api/conflict");
        var value = await response.Content.ReadFromJsonAsync<Contact>();
    
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        value.Should().BeEquivalentTo(Contact.Default);
    }
}