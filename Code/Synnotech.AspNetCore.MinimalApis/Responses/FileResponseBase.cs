using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Base abstract class for FileResponse classes.
/// </summary>
public abstract class FileResponseBase
{
    private readonly string? _fileDownloadName;

    /// <summary>
    /// Creates a new <see cref="FileResponseBase"/> instance
    /// with the provided <paramref name="contentType"/>.
    /// </summary>
    /// <param name="contentType">The Content-Type header of the response.</param>
    protected FileResponseBase(string? contentType)
    {
        ContentType = contentType ?? "application/octet-stream";
    }

    /// <summary>
    /// Gets the Content-Type header for the response.
    /// </summary>
    public string ContentType { get; }

    /// <summary>
    /// Gets the file name that will be used in the Content-Disposition header
    /// of the response.
    /// </summary>
    [AllowNull]
    public string FileDownloadName
    {
        get => _fileDownloadName ?? string.Empty;
        init => _fileDownloadName = value;
    }

    /// <summary>
    /// Gets or sets the etag associated with the <see cref="FileResponseBase"/>.
    /// </summary>
    public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Gets the etag associated with the <see cref="FileResponseBase"/>.
    /// </summary>
    public EntityTagHeaderValue? EntityTag { get; init; }

    /// <summary>
    /// Gets the value that enables range processing for the <see cref="FileResponseBase"/>.
    /// </summary>
    public bool EnableRangeProcessing { get; init; }

    /// <summary>
    /// Gets or sets the FileLength in the response header.
    /// </summary>
    public long? FileLength { get; set; }
}