using System;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

/// <summary>
/// Provides infos for FileResponse classes.
/// </summary>
public readonly struct FileResponseInfo
{
    /// <summary>
    /// Gets or sets the specified MIME content type.
    /// </summary>
    public string ContentType { get; init; }

    /// <summary>
    /// Gets or sets the download name for the provided file.
    /// </summary>
    public string FileDownloadName { get; init; }

    /// <summary>
    /// Gets or sets the date and time where the file was last modified.
    /// </summary>
    public DateTimeOffset? LastModified { get; init; }

    /// <summary>
    /// Gets or sets the Etag Value of the HTTP header.
    /// </summary>
    public EntityTagHeaderValue? EntityTag { get; init; }

    /// <summary>
    /// Gets or sets the EnableRangeProcessing value.
    /// </summary>
    public bool EnableRangeProcessing { get; init; }
}