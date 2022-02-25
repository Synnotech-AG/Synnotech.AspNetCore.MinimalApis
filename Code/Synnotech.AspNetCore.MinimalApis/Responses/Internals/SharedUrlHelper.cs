using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

/// <summary>
/// Helps parsing Url to right format before sharing in HTTP response.
/// </summary>
public static class SharedUrlHelper
{
    /// <summary>
    /// Helps parsing the <paramref name="contentPath"/> to work on any machine
    /// by adding the application path to the provided path.
    /// </summary>
    /// <param name="httpContext">The current http context.</param>
    /// <param name="contentPath">The contentPath that should be checked.</param>
    public static string? Content(HttpContext httpContext, string? contentPath)
    {
        if (string.IsNullOrEmpty(contentPath))
            return null;
        
        if (contentPath[0] == '~')
        {
            var segment = new PathString(contentPath.Substring(1));
            var applicationPath = httpContext.Request.PathBase;

            var path = applicationPath.Add(segment);
            Debug.Assert(path.HasValue);
            return path.Value;
        }

        return contentPath;
    }

    /// <summary>
    /// Checks if the provided <paramref name="url"/> is a local url.
    /// </summary>
    /// <param name="url">The url that should be checked.</param>
    public static bool IsLocalUrl([NotNullWhen(true)] string? url)
    {
        if (string.IsNullOrEmpty(url))
            return false;

        // Allows "/" or "/foo" but not "//" or "/\".
        if (url[0] == '/')
        {
            // url is exactly "/"
            if (url.Length == 1)
                return true;

            // url doesn't start with "//" or "/\"
            if (url[1] != '/' && url[1] != '\\')
                return !HasControlCharacter(url.AsSpan(1));

            return false;
        }

        // Allows "~/" or "~/foo" but not "~//" or "~/\".
        if (url[0] == '~' && url.Length > 1 && url[1] == '/')
        {
            // url is exactly "~/"
            if (url.Length == 2)
                return true;

            // url doesn't start with "~//" or "~/\"
            if (url[2] != '/' && url[2] != '\\')
                return !HasControlCharacter(url.AsSpan(2));

            return false;
        }

        return false;

        static bool HasControlCharacter(ReadOnlySpan<char> readOnlySpan)
        {
            // URLs may not contain ASCII control characters.
            for (var i = 0; i < readOnlySpan.Length; i++)
            {
                if (char.IsControl(readOnlySpan[i]))
                    return true;
            }

            return false;
        }
    }
}