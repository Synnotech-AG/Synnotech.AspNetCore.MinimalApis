using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseRedirectStatusTests : BaseWebAppTest
{
    public HttpResponseRedirectStatusTests(ITestOutputHelper output) : base(output) { }

    // Status Code 307 Temporary Redirect

    // [Fact]
    // public async Task RedirectTemporaryTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/redirect/temporary");
    //     var responseLocation = getRedirectUriFormHttpResponseMessage(response);
    //
    //     // statusCodes always is 404 because the redirect url can not be found
    //     response.Should().NotBeNull();
    //     responseLocation.Should().NotBeNull().And.Be(Location);
    // }
    //
    // // Status Code 308 Permanent Redirect
    //
    // [Fact]
    // public async Task RedirectPermanentTest()
    // {
    //     var response = await GetHttpResponseMessageFromApi("/redirect/permanent");
    //     var responseLocation = getRedirectUriFormHttpResponseMessage(response);
    //
    //     // statusCodes always is 404 because the redirect url can not be found
    //     response.Should().NotBeNull();
    //     responseLocation.Should().NotBeNull().And.Be(Location);
    // }

    private Uri? getRedirectUriFormHttpResponseMessage(HttpResponseMessage response)
    {
        return response.RequestMessage?.RequestUri;
    }
}