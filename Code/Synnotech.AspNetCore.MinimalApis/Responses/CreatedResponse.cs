using System;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 201 Created response.
/// </summary>
public class CreatedResponse : StatusCodeResponseWithLocation
{
    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    public CreatedResponse() : base(StatusCodes.Status201Created) { }

    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    public CreatedResponse(string url) : base(StatusCodes.Status201Created, url) { }

    /// <summary>
    /// Initializes a new instance of <see cref="AcceptedResponse" />.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
    public CreatedResponse(Uri url) : base(StatusCodes.Status201Created, url) { }
}