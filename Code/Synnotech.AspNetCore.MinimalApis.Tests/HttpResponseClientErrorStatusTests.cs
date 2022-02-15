using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseClientErrorStatusTests : HttpResponseTestsBase
{
    // Status Code 400 Bad Request
    [Fact]
    public async Task BadRequestWithoutMessageTest()
    {
        var response = await GetHttpResponseMessageFromApi("/badRequest");
        var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseValue.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task BadRequestWithMessageTest()
    {
        var response = await GetHttpResponseMessageFromApi("/badRequest/string");
        var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    }

    // Status Code 401 Unauthorized
    [Fact]
    public async Task UnauthorizedTest()
    {
        var response = await GetHttpResponseMessageFromApi("/Unauthorized");
        var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseValue.Should().BeNullOrEmpty();
    }

    // Status Code 403 Forbidden
    [Fact]
    public async Task ForbiddenWithoutMessageTest()
    {
        var response = await GetHttpResponseMessageFromApi("/Forbidden");
        var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);

        // TODO: currently internal server error as response code, should be 403 forbidden
        // debug shows that the factoryMethod, ForbiddenResponse-Constructor and testApi work as intended
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        responseValue.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ForbiddenWithAuthenticationSchemeAsStringTest()
    {
        var response = await GetHttpResponseMessageFromApi("/forbidden/authenticationScheme/string");
        // TODO: test if authenticationProperties and scheme get sent in the response

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    // Status Code 409 Conflict
    [Fact]
    public async Task ConflictTest()
    {
        var response = await GetHttpResponseMessageFromApi("/conflict");
        var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    }
}