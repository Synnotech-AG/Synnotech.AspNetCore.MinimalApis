using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
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

    // [Fact]
    // public async Task UnauthorizedTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/Unauthorized");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    //     responseValue.Should().BeNullOrEmpty();
    // }
    //
    // // Status Code 403 Forbidden
    //
    // [Fact]
    // public async Task ForbiddenWithoutMessageTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/Forbidden");
    //     var responseStandard = await GetHttpResponseMessageFromApi("/Forbidden/standard");
    //
    //     // internal server error as response code, should be 403 forbidden
    //     // no service registered for IAuthenticationService -> Exception -> InternalServerError
    //     // internal AspNetCore-class returns the same errorCode (500)
    //
    //     response.StatusCode.Should().Be(responseStandard.StatusCode);
    // }
    //
    // [Fact]
    // public async Task ForbiddenWithAuthenticationSchemeAsStringTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/forbidden/authenticationScheme/string");
    //     
    //     response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    // }
    //
    // [Fact]
    // public async Task ForbiddenWithAuthenticationPropertiesAndSchemeAsStringTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/forbidden/authenticationProperties/string");
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    // }
    //
    // // Status Code 409 Conflict
    //
    // [Fact]
    // public async Task ConflictTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/conflict");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    // }
}