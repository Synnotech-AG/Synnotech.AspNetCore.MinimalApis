using System;
using Light.GuardClauses;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

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
}