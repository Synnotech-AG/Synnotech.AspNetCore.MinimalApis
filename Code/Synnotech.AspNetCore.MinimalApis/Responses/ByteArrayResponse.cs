using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an <see cref="FileResponseBase" /> that when executed will
/// write a ByteArray file to the response.
/// </summary>
public sealed class ByteArrayResponse : FileResponse
{
    /// <summary>
    /// Creates a new <see cref="ByteArrayResponse" /> instance with
    /// the provided <paramref name="fileContents" /> and the
    /// provided <paramref name="contentType" />.
    /// </summary>
    /// <param name="fileContents">The bytes that represent the file content.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    public ByteArrayResponse(ReadOnlyMemory<byte> fileContents, string? contentType) : base(contentType)
    {
        FileContents = fileContents;
        FileLength = fileContents.Length;
    }

    /// <summary>
    /// Gets the file contents.
    /// </summary>
    public ReadOnlyMemory<byte> FileContents { get; }

    /// <inheritdoc />
    protected override Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength)
    {
        return FileResponseHelper.WriteFileAsync(httpContext, FileContents, range, rangeLength);
    }
}