using System;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 202 Accepted response.
/// </summary>
public class AcceptedResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    public AcceptedResponse() : base(StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    public AcceptedResponse(string url) : base(StatusCodes.Status202Accepted) => Url = url;

    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
    public AcceptedResponse(Uri url) : base(StatusCodes.Status202Accepted) => Url = url.PrepareForLocationHeader();

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