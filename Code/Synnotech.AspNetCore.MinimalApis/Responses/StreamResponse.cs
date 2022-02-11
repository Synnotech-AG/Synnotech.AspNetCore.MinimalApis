using System.IO;
using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Synnotech.AspNetCore.MinimalApis.Responses.Internals;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an <see cref="FileResponseBase"/> that when executed will
/// write a file from a stream to the response.
/// </summary>
public sealed class StreamResponse : FileResponse
{
    /// <summary>
    /// Creates a new <see cref="StreamResponse"/> instance with
    /// the provided <paramref name="fileStream"/> and the
    /// provided <paramref name="contentType"/>.
    /// </summary>
    /// <param name="fileStream">The stream with the file.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    public StreamResponse(Stream fileStream, string? contentType) : base(contentType)
    {
        fileStream.MustNotBeNull();

        FileStream = fileStream;
        if (fileStream.CanSeek)
        {
            FileLength = fileStream.Length;
        }
    }

    /// <summary>
    /// Gets the stream with the file that will be sent back as the response.
    /// </summary>
    public Stream FileStream { get; init; }

    /// <inheritdoc />
    public override async Task ExecuteAsync(HttpContext httpContext)
    {
        await using (FileStream)
        {
            await base.ExecuteAsync(httpContext);
        }
    }

    /// <inheritdoc />
    protected override Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength)
    {
        return FileResponseHelper.WriteFileAsync(httpContext, FileStream, range, rangeLength);
    }
}