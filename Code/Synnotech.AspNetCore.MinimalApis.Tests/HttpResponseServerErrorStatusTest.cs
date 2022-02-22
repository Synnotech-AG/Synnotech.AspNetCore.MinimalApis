using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseServerErrorStatusTest : BaseWebAppTest
{
    public HttpResponseServerErrorStatusTest(ITestOutputHelper output) : base(output) { }

        // Status Code 500 Internal Server Error
    // [Fact]
    // public async Task InternalServerErrorTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/internalServerError");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    // }
}