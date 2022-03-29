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
    public static void ObjectResponseMustImplementIHasBody()
    {
        typeof(ObjectResponse<>).Should().Implement<IHasBody>();
        typeof(ObjectResponse<string>).Should().Implement<IHasBody<string>>();
    }

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

    [Fact]
    public static void RetrieveBodyOfObjectResponse()
    {
        IResult objectResponse = new ObjectResponse<string>("Foo", StatusCodes.Status200OK);

        var bodyObject = objectResponse.GetBody();
        var bodyString = objectResponse.GetBody<string>();

        bodyString.Should().BeSameAs("Foo");
        bodyObject.Should().BeSameAs(bodyString);
    }
}