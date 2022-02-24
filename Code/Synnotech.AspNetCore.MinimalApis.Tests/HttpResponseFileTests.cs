using System;
using FluentAssertions;
using Microsoft.AspNetCore.StaticFiles;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;
using Xunit;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class HttpResponseFileTests : BaseWebAppTest
{
    public HttpResponseFileTests(ITestOutputHelper output) : base(output) { }

    // Stream Response
    [Fact]
    public void StreamResponseTest()
    {
        using var response = HttpClient.GetStreamAsync("/api/stream");
        var streamLength = FileReadHelper.GetStreamLength(response.Result);

        response.Should().NotBeNull();
        streamLength.Should().Be(FileReadHelper.GetFileLength(TestFile.ExcelDefault.FilePath!));
    }

    // Byte Array Response
    [Fact]
    public void ByteArrayResponseTest()
    {
        using var response = HttpClient.GetAsync("/api/bytes");

        response.Should().NotBeNull();
    }
}