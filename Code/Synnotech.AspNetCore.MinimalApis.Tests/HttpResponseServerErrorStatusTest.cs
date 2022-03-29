using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseServerErrorStatusTest : BaseWebAppTest
{
    public HttpResponseServerErrorStatusTest(ITestOutputHelper output) : base(output) { }

    // Status Code 500 Internal Server Error

    [Fact]
    public async Task InternalServerError()
    {
        using var response = await HttpClient.GetAsync("/api/internalServerError");

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        await response.ShouldHaveNoContentAsync();
    }

    [Fact]
    public async Task InternalServerErrorWithBody()
    {
        using var response = await HttpClient.GetAsync("/api/internalServerError/withBody");

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        var value = await response.Content.ReadFromJsonAsync<Contact>();
        value.Should().BeEquivalentTo(Contact.Default);
    }
}