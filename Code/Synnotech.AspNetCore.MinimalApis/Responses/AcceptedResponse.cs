using System;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 202 Accepted response.
/// </summary>
public class AcceptedResponse : StatusCodeResponseWithLocation
{
    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    public AcceptedResponse() : base(StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    public AcceptedResponse(string url) : base(StatusCodes.Status202Accepted, url) { }

    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
    public AcceptedResponse(Uri url) : base(StatusCodes.Status202Accepted, url) { }
}