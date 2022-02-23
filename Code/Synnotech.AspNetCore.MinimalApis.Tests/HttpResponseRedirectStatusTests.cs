using System;
using System.Net;
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
    [Fact]
    public async Task RedirectTemporaryTest()
    {
        using var response = await HttpClient.GetAsync("/api/redirect/temporary");
        var responseLocation = GetRedirectUriFormHttpResponseMessage(response);

        // Status Code should be 200 Ok because the redirect routes to this Url
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseLocation.Should().NotBeNull();
        responseLocation!.AbsoluteUri.Should().BeEquivalentTo(Location.DefaultRedirect.Url);
    }

    // Status Code 308 Permanent Redirect
    [Fact]
    public async Task RedirectPermanentTest()
    {
        using var response = await HttpClient.GetAsync("/api/redirect/permanent");
        var responseLocation = GetRedirectUriFormHttpResponseMessage(response);

        // Status Code should be 200 Ok because the redirect routes to this Url
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseLocation.Should().NotBeNull();
        responseLocation!.AbsoluteUri.Should().BeEquivalentTo(Location.DefaultRedirect.Url);
    }

    private Uri? GetRedirectUriFormHttpResponseMessage(HttpResponseMessage response)
    {
        return response.RequestMessage?.RequestUri;
    }
}