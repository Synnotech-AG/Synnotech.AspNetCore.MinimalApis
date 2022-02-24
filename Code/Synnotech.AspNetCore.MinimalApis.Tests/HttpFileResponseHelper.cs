using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Synnotech.AspNetCore.MinimalApis.Responses;
using Xunit.Abstractions;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class HttpFileResponseHelper
{
    public static StreamResponse SetupStreamResponse()
    {
        var (contentType, path) = ProvideContentTypeAndPath();

        var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

        return Response.Stream(fileStream, contentType);
    }

    public static ByteArrayResponse SetupByteArrayResponse()
    {
        var (contentType, path) = ProvideContentTypeAndPath();

        var bytes = File.ReadAllBytes(path);

        return Response.ByteArray(bytes, contentType);
    }

    private static (string? contentType, string path) ProvideContentTypeAndPath()
    {
        var fileName = @"DefaultValues\TestFile.xlsx";

        // splitting end of string to get to the desired path
        var environmentDirectory = (Environment.CurrentDirectory).Split(@"bin\Debug")[0];

        var path = Path.Combine(environmentDirectory, fileName);

        new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

        return (contentType, path);
    }
}