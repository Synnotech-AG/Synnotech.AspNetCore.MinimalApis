using System;
using System.IO;
using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Synnotech.AspNetCore.MinimalApis.Responses.Internals;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an <see cref="FileResponseBase" /> that when executed will
/// write a file from a stream to the response.
/// </summary>
public sealed class StreamResponse : FileResponse
{
    /// <summary>
    /// Creates a new <see cref="StreamResponse" /> instance with
    /// the provided <paramref name="stream" /> and the
    /// provided <paramref name="contentType" />.
    /// </summary>
    /// <param name="stream">The stream with the file.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="stream" /> is null.</exception>
    public StreamResponse(Stream stream, string? contentType) : base(contentType)
    {
        stream.MustNotBeNull();

        Stream = stream;
        if (stream.CanSeek)
        {
            FileLength = stream.Length;
        }
    }

    /// <summary>
    /// Gets the stream with the file that will be sent back as the response.
    /// </summary>
    public Stream Stream { get; }

    /// <inheritdoc />
    public override async Task ExecuteAsync(HttpContext httpContext)
    {
        await base.ExecuteAsync(httpContext);
    }

    /// <inheritdoc />
    protected override Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength)
    {
        return FileResponseHelper.WriteFileAsync(httpContext, Stream, range, rangeLength);
    }
}