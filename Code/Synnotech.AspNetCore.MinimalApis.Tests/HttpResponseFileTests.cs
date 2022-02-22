using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseFileTests : BaseWebAppTest
{
    public HttpResponseFileTests(ITestOutputHelper output) : base(output) { }
    
    // Stream Response
    // [Fact]
    // public void StreamResponseTest()
    // {
    //     var response = GetHttpResponseMessageFromApi("/api/stream");
    //
    //     response.Should().NotBeNull();
    // }
    //
    // // Byte Array Response
    // [Fact]
    // public void ByteArrayResponseTest()
    // {
    //     var response = GetHttpResponseMessageFromApi("/api/bytes");
    //
    //     response.Should().NotBeNull();
    // }
}