using System.Threading.Tasks;
using FluentAssertions;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseFileTests : BaseWebAppTest
{
    public HttpResponseFileTests(ITestOutputHelper output) : base(output)
    {
        _testFileByteLength = FileHelper.GetFileLength(TestFile.ExcelDefault.FilePath!);
    }

    private readonly long _testFileByteLength;

    // Stream Response
    [Fact]
    public async Task StreamResponseTest()
    {
        await using var stream = await HttpClient.GetStreamAsync("/api/stream");
        var streamLength = FileHelper.GetStreamLength(stream);

        stream.Should().NotBeNull();
        streamLength.Should().Be(_testFileByteLength);
    }

    // Byte Array Response
    [Fact]
    public async Task ByteArrayResponseTest()
    {
        var array = await HttpClient.GetByteArrayAsync("/api/bytes");
        var bytesLength = array.LongLength;

        array.Should().NotBeNull();
        bytesLength.Should().Be(_testFileByteLength);
    }

    // Physical File Response
    [Fact]
    public async Task PhysicalFileResponseTest()
    {
        await using var file = await HttpClient.GetStreamAsync("/api/physical");
        var physicalFileLength = FileHelper.GetStreamLength(file);

        file.Should().NotBeNull();
        physicalFileLength.Should().Be(_testFileByteLength);
    }

    // Virtual File Response
    [Fact]
    public async Task VirtualFileResponseTest()
    {
        await using var file = await HttpClient.GetStreamAsync("/api/virtual");
        var virtualFileLength = FileHelper.GetStreamLength(file);

        file.Should().NotBeNull();
        virtualFileLength.Should().Be(_testFileByteLength);
    }
}