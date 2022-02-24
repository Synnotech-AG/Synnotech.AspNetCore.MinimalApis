using System.Threading.Tasks;
using FluentAssertions;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseFileTests : BaseWebAppTest
{
    public HttpResponseFileTests(ITestOutputHelper output) : base(output) { }

    // Stream Response
    [Fact]
    public async Task StreamResponseTest()
    {
        await using var stream = await HttpClient.GetStreamAsync("/api/stream");
        var streamLength = FileReadHelper.GetStreamLength(stream);

        stream.Should().NotBeNull();
        streamLength.Should().Be(FileReadHelper.GetFileLength(TestFile.ExcelDefault.FilePath!));
    }

    // Byte Array Response
    [Fact]
    public async Task ByteArrayResponseTest()
    {
        var array = await HttpClient.GetByteArrayAsync("/api/bytes");
        var bytesLength = array.LongLength;

        array.Should().NotBeNull();
        bytesLength.Should().Be(FileReadHelper.GetFileLength(TestFile.ExcelDefault.FilePath!));
    }
}