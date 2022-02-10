using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 401 Unauthorized response.
/// </summary>
public sealed class UnauthorizedResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="UnauthorizedResponse"/>.
    /// </summary>
    public UnauthorizedResponse() : base(StatusCodes.Status401Unauthorized) { }
}