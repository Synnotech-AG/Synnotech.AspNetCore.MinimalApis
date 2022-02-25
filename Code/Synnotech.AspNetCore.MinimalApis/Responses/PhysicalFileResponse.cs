using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Synnotech.AspNetCore.MinimalApis.Responses.Internals;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// A <see cref="PhysicalFileResponse" /> on execution will write a file from disk to the response
/// using mechanisms provided by the host.
/// </summary>
public class PhysicalFileResponse : FileResponse
{
    /// <summary>
    /// Creates a new <see cref="PhysicalFileResponse" /> instance with the
    /// provided <paramref name="fileName" /> and the provided <paramref name="contentType" />.
    /// </summary>
    /// <param name="fileName">The path to the file. The path must be an absolute path.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    public PhysicalFileResponse(string fileName, string? contentType) : base(contentType)
    {
        FileName = fileName;
    }

    /// <summary>
    /// Gets the path to the file that will be sent back as the response.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Gets the <see cref="FileInfoWrapper" /> that contains infos for the provided file.
    /// </summary>
    public Func<string, FileInfoWrapper> GetFileInfoWrapper { get; init; } =
        static path => new FileInfoWrapper(path);

    /// <inheritdoc />
    public override Task ExecuteAsync(HttpContext httpContext)
    {
        var fileInfo = GetFileInfoWrapper(FileName);
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException($"Could not find file: {FileName}", FileName);
        }

        LastModified ??= fileInfo.LastWriteTimeUtc;
        FileLength = fileInfo.Length;

        return base.ExecuteAsync(httpContext);
    }

    /// <inheritdoc />
    protected override Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength)
    {
        var response = httpContext.Response;
        if (!Path.IsPathRooted(FileName))
        {
            throw new NotSupportedException($"Path '{FileName}' was not rooted.");
        }

        var offset = 0L;
        var count = (long?) null;

        if (range != null)
        {
            offset = range.From ?? 0L;
            count = rangeLength;
        }

        return response.SendFileAsync(
            FileName,
            offset: offset,
            count: count
        );
    }
}