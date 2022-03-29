using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 500 Internal Server Error response.
/// </summary>
public class InternalServerErrorResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="InternalServerErrorResponse" />.
    /// </summary>
    public InternalServerErrorResponse() : base(StatusCodes.Status500InternalServerError) { }
}