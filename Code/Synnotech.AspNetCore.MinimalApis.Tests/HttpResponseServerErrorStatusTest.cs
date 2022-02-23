using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseServerErrorStatusTest : BaseWebAppTest
{
    public HttpResponseServerErrorStatusTest(ITestOutputHelper output) : base(output) { }

    // Status Code 500 Internal Server Error
    [Fact]
    public async Task InternalServerErrorTest()
    {
        using var response = await HttpClient.GetAsync("/api/internalServerError");
        var value = await response.Content.ReadFromJsonAsync<Contact>();

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        value.Should().BeEquivalentTo(Contact.Default);
    }
}