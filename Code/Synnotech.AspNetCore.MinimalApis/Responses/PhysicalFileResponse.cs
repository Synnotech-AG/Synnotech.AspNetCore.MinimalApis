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
    /// provided <paramref name="filePath" /> and the provided <paramref name="contentType" />.
    /// </summary>
    /// <param name="filePath">The path to the file. The path must be an absolute path.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="filePath"/> is null.</exception>
    public PhysicalFileResponse(string filePath, string? contentType) : base(contentType)
    {
        FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    /// <summary>
    /// Gets the path to the file that will be sent back as the response.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets the <see cref="FileInfoWrapper" /> that contains infos for the provided file.
    /// </summary>
    public Func<string, FileInfoWrapper> GetFileInfoWrapper { get; init; } =
        static path => new FileInfoWrapper(path);

    /// <inheritdoc />
    public override Task ExecuteAsync(HttpContext httpContext)
    {
        var fileInfo = GetFileInfoWrapper(FilePath);
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException($"Could not find file: {FilePath}", FilePath);
        }

        LastModified ??= fileInfo.LastWriteTimeUtc;
        FileLength = fileInfo.Length;

        return base.ExecuteAsync(httpContext);
    }

    /// <inheritdoc />
    protected override Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength)
    {
        var response = httpContext.Response;
        if (!Path.IsPathRooted(FilePath))
        {
            throw new NotSupportedException($"Path '{FilePath}' was not rooted.");
        }

        var offset = 0L;
        var count = (long?) null;

        if (range != null)
        {
            offset = range.From ?? 0L;
            count = rangeLength;
        }

        return response.SendFileAsync(
            FilePath,
            offset: offset,
            count: count
        );
    }
}