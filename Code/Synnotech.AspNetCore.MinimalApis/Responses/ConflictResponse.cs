using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 409 Conflict response.
/// </summary>
public class ConflictResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="ConflictResponse" />.
    /// </summary>
    public ConflictResponse() : base(StatusCodes.Status409Conflict) { }
}