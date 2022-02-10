using System;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

internal readonly struct FileResponseInfo
{
    public string ContentType { get; init; }

    public string FileDownloadName { get; init; }

    public DateTimeOffset? LastModified { get; init; }

    public EntityTagHeaderValue? EntityTag { get; init; }

    public bool EnableRangeProcessing { get; init; }
}