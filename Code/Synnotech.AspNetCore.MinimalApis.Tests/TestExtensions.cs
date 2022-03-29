using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class TestExtensions
{
    public static async Task ShouldHaveNoContentAsync(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        content.Should().BeNullOrEmpty();
    }
}