using System;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

public static class ResponseContentTypeHelper
{
    public static void ResolveContentTypeAndEncoding(
        string? actionResultContentType,
        string? httpResponseContentType,
        (string defaultContentType, Encoding defaultEncoding) @default,
        Func<string, Encoding> getEncoding,
        out string resolvedContentType,
        out Encoding resolvedContentTypeEncoding
    )
    {
        var (defaultContentType, defaultContentTypeEncoding) = @default;

        // 1. User sets the ContentType property on the action result
        if (actionResultContentType != null)
        {
            resolvedContentType = actionResultContentType;
            var actionResultEncoding = getEncoding(actionResultContentType);
            resolvedContentTypeEncoding = actionResultEncoding ?? defaultContentTypeEncoding;

            return;
        }

        // 2. User sets the ContentType property on the http response directly
        if (!string.IsNullOrEmpty(httpResponseContentType))
        {
            var mediaTypeEncoding = getEncoding(httpResponseContentType);
            if (mediaTypeEncoding != null)
            {
                resolvedContentType = httpResponseContentType;
                resolvedContentTypeEncoding = mediaTypeEncoding;
            }
            else
            {
                resolvedContentType = httpResponseContentType;
                resolvedContentTypeEncoding = defaultContentTypeEncoding;
            }

            return;
        }

        // 3. Fallback to the default content type
        resolvedContentType = defaultContentType;
        resolvedContentTypeEncoding = defaultContentTypeEncoding;
    }

    public static Encoding? GetEncoding(string mediaType)
    {
        if (MediaTypeHeaderValue.TryParse(mediaType, out var parsed))
        {
            return parsed.Encoding;
        }

        return default;
    }
}