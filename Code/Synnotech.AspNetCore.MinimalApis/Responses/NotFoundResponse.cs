using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 404 Not Found response.
/// </summary>
public class NotFoundResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="NotFoundResponse" />.
    /// </summary>
    public NotFoundResponse() : base(StatusCodes.Status404NotFound) { }
}