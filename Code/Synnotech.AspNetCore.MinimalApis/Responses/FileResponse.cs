using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Synnotech.AspNetCore.MinimalApis.Responses.Internals;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an HTTP response that includes a file.
/// </summary>
public abstract class FileResponse : FileResponseBase, IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="FileResponse"/>.
    /// </summary>
    /// <param name="contentType">The Content-Type header of the response.</param>
    protected FileResponse(string? contentType) : base(contentType) { }

    /// <summary>
    /// Write an HTTP response reflecting the result.
    /// </summary>
    /// <param name="httpContext">Encapsulates all HTTP specific
    /// information about an individual HTTP request.</param>
    /// <param name="range">The byte range of the provided file.</param>
    /// <param name="rangeLength">The length of the provided file in bytes.</param>
    protected abstract Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength);

    /// <inheritdoc />
    public virtual Task ExecuteAsync(HttpContext httpContext)
    {
        var fileResultInfo = new FileResponseInfo
        {
            ContentType = ContentType,
            EnableRangeProcessing = EnableRangeProcessing,
            EntityTag = EntityTag,
            FileDownloadName = FileDownloadName,
            LastModified = LastModified
        };

        var (range, rangeLength, serveBody) = FileResponseHelper.SetHeaders(
            httpContext,
            fileResultInfo,
            FileLength,
            EnableRangeProcessing,
            LastModified,
            EntityTag);

        if (!serveBody)
        {
            return Task.CompletedTask;
        }

        if (range != null && rangeLength == 0)
        {
            return Task.CompletedTask;
        }

        return ExecuteCoreAsync(httpContext, range, rangeLength);
    }
}