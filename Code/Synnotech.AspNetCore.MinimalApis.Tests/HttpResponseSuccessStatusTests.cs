using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseSuccessStatusTests : BaseWebAppTest
{
    public HttpResponseSuccessStatusTests(ITestOutputHelper output) : base(output) { }

    // // Status Code 200 OK
    // [Fact]
    // public async Task OkWithoutBodyTest()
    // {
    //     // act
    //     var response = await GetHttpResponseMessageFromApi("/ok");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //
    //     // assert
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    //     responseValue.Should().BeNullOrEmpty();
    // }
    //
    // [Fact]
    // public async Task OkWithBodyTest()
    // {
    //     // act
    //     var response = await GetHttpResponseMessageFromApi("/ok/body");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //
    //     // assert
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    // }
    //
    // // Status Code 201 Created
    // [Fact]
    // public async Task CreatedWithStringAsLocationTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/created/string");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //     var responseUrl = GetUriFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Created);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    //     responseUrl.Should().NotBeNull().And.Be(Location);
    // }
    //
    // [Fact]
    // public async Task CreatedWithUriAsLocationTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/created/uri");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //     var responseUrl = GetUriFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Created);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    //     responseUrl.Should().NotBeNull().And.Be(Location);
    // }
    //
    // // Status Code 202 Accepted
    // [Fact]
    // public async Task AcceptedWithoutLocationProvidedTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/accepted");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    //     responseValue.Should().NotBeNullOrEmpty();
    //     responseValue.Should().NotBeNull().And.Be(Value);
    // }
    //
    // [Fact]
    // public async Task AcceptedWithStringAsLocationTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/accepted/string");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //     var responseUrl = GetUriFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    //     responseUrl.Should().NotBeNull().And.Be(Location);
    // }
    //
    // [Fact]
    // public async Task AcceptedWithUriAsLocationTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/accepted/uri");
    //     var responseValue = await GetAndFormatStringContentFromHttpResponseMessage(response);
    //     var responseUrl = GetUriFromHttpResponseMessage(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    //     responseValue.Should().NotBeNullOrEmpty().And.Be(Value);
    //     responseUrl.Should().NotBeNull().And.Be(Location);
    // }
    //
    //
    // // Status Code 204 No Content
    // [Fact]
    // public async Task NoContentTest()
    // {
    //     // act
    //     var response = await GetHttpResponseMessageFromApi("/noContent");
    //
    //     // assert
    //     response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    // }
    //
    // private Uri? GetUriFromHttpResponseMessage(HttpResponseMessage response)
    // {
    //     return response.Headers.Location;
    // }
}