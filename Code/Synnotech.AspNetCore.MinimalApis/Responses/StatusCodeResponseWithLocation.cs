using System;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents a status code response that sets the "Location" header.
/// </summary>
public abstract class StatusCodeResponseWithLocation : StatusCodeResponse, IHasLocationUrl
{
    /// <summary>
    /// Initializes a new instance of <see cref="StatusCodeResponseWithLocation" />.
    /// </summary>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    protected StatusCodeResponseWithLocation(int statusCode) : base(statusCode) { }

    /// <summary>
    /// Initializes a new instance of <see cref="StatusCodeResponseWithLocation" />.
    /// </summary>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    protected StatusCodeResponseWithLocation(int statusCode, string url) : base(statusCode) => Url = url;

    /// <summary>
    /// Initializes a new instance of <see cref="StatusCodeResponseWithLocation" />.
    /// </summary>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
    protected StatusCodeResponseWithLocation(int statusCode, Uri url) : base(statusCode) => Url = url.PrepareForLocationHeader();

    /// <summary>
    /// Gets the URL where the accepted content can be found.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Sets the "Location" header on the HTTP response if a URL was passed to this response.
    /// </summary>
    protected override void ConfigureResponse(HttpContext httpContext) =>
        httpContext.TrySetLocationHeader(Url);
}