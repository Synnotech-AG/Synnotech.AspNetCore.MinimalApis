using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 403 Forbidden response.
/// </summary>
public class ForbiddenResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />.
    /// </summary>
    public ForbiddenResponse() : base(StatusCodes.Status403Forbidden) { }
}