using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpRespondTest
{
    private IWebHost app;
    private HttpClient client;

    const string value = "Test";
    const string url = "test.url";

    [Fact]
    public async Task OkWithoutBodyTest()
    {
        // arrange
        SetupWebApplication();

        // act
        var okResponse = await client.GetAsync("/api/ok");

        // assert
        okResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        okResponse.Content.ReadAsStringAsync().Result.Should().Be(value);
    }

    private void SetupWebApplication()
    {
        app = new WebHostBuilder()
             .Configure(routing =>
                  {
                      routing.UseRouting();
                      routing.UseEndpoints(router => router.AddStatusCodeResponses()
                                                           .AddObjectResponses()
                                                           .AddRedirectAndForbiddenResponses()
                      );
                  }
              )
             .Build();

        client = app.GetTestClient();
    }
}