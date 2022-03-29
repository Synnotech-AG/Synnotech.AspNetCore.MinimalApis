using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 204 No Content response.
/// </summary>
public class NoContentResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="NoContentResponse" />.
    /// </summary>
    public NoContentResponse() : base(StatusCodes.Status204NoContent) { }
}