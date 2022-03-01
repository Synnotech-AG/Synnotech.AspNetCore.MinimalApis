using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Synnotech.AspNetCore.MinimalApis.Responses;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;

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

    public static FileResponse SetupPhysicalFileResponse()
    {
        var (contentType, path) = ProvideContentTypeAndPath();

        return Response.File(path, contentType);
    }

    public static FileResponse SetupVirtualFileResponse()
    {
        var (contentType, path) = ProvideContentTypeAndPath();

        return Response.File(path, contentType);
    }

    private static (string? contentType, string path) ProvideContentTypeAndPath()
    {
        new FileExtensionContentTypeProvider().TryGetContentType(TestFile.ExcelDefault.FileName!, out var contentType);

        return (contentType, TestFile.ExcelDefault.FilePath!);
    }
}