using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseServerErrorStatusTest : HttpResponseTestsBase
{
    // Status Code 500 Internal Server Error
    [Fact]
    public async Task InternalServerErrorTest()
    {
        var response = await GetHttpResponseMessageFromApi("/internalServerError");
        var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    }
}