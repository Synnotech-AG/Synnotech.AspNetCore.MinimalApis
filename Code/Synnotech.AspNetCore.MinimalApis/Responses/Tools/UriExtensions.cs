using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Tools;

/// <summary>
/// Provides extension methods for the <see cref="Uri" /> class.
/// </summary>
public static class UriExtensions
{
    /// <summary>
    /// Either returns the absolute URI as a string or the components
    /// of a relative URI. The returned string can be used for the location header
    /// in HTTP responses.
    /// </summary>
    /// <param name="url">The URL which should be prepared </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    public static string PrepareForLocationHeader(this Uri url) =>
        url.MustNotBeNull().IsAbsoluteUri ?
            url.AbsoluteUri :
            url.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);

    /// <summary>
    /// Tries to set the specified URL as the "Location" header on the response of the given HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context whose response will be manipulated.</param>
    /// <param name="url">The URL to be set. If this string is null, empty, or contains only white space, the header will not be set.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpContext" /> is null.</exception>
    public static void TrySetLocationHeader(this HttpContext httpContext, string? url) =>
        httpContext.MustNotBeNull().Response.TrySetLocationHeader(url);

    /// <summary>
    /// Tries to set the specified URL as the "Location" header on the HTTP response.
    /// </summary>
    /// <param name="httpResponse">The response whose "Location" header will be set if necessary.</param>
    /// <param name="url">The URL to be set. If this string is null, empty, or contains only white space, the header will not be set.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpResponse" /> is null.</exception>
    public static void TrySetLocationHeader(this HttpResponse httpResponse, string? url)
    {
        if (!url.IsNullOrWhiteSpace())
            httpResponse.MustNotBeNull().Headers.Location = url;
    }
}