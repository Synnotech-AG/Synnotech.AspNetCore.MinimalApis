using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 400 Bad Request response without body.
/// </summary>
public class BadRequestResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="BadRequestResponse" />.
    /// </summary>
    public BadRequestResponse() : base(StatusCodes.Status400BadRequest) { }
}