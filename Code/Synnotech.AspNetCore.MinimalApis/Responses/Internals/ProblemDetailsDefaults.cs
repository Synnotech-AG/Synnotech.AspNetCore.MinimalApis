using System.Collections.Generic;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

/// <summary>
/// Provides default messages for HTTP error codes ranging from 400 to 500.
/// </summary>
internal static class ProblemDetailsDefaults
{
    public static readonly Dictionary<int, (string Type, string Title)> Defaults = new ()
    {
        [400] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                "Bad Request"
            ),

        [401] =
            (
                "https://tools.ietf.org/html/rfc7235#section-3.1",
                "Unauthorized"
            ),

        [403] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                "Forbidden"
            ),

        [404] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                "Not Found"
            ),

        [406] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.5.6",
                "Not Acceptable"
            ),

        [409] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                "Conflict"
            ),

        [415] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.5.13",
                "Unsupported Media Type"
            ),

        [422] =
            (
                "https://tools.ietf.org/html/rfc4918#section-11.2",
                "Unprocessable Entity"
            ),

        [500] =
            (
                "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                "An error occurred while processing your request."
            ),
    };
}