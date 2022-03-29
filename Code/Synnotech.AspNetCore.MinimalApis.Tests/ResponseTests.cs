using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses;
using Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class ResponseTests
{
    [Fact]
    public static void ObjectResponseMustImplementIHasStatusCode() =>
        typeof(ObjectResponse<>).Should().Implement<IHasStatusCode>();

    [Fact]
    public static void StatusCodeResponseMustImplementIHasStatusCode() =>
        typeof(StatusCodeResponse).Should().Implement<IHasStatusCode>();

    [Fact]
    public static void RetrieveStatusCodeOfObjectResponse()
    {
        IResult objectResponse = new ObjectResponse<string>("Foo", StatusCodes.Status200OK);

        var statusCode = objectResponse.GetStatusCode();

        statusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public static void RetrieveStatusCodeOfStatusCodeResponse()
    {
        IResult statusCodeResponse = new StatusCodeResponse(StatusCodes.Status401Unauthorized);

        var statusCode = statusCodeResponse.GetStatusCode();

        statusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }
}