using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 200 OK response without body.
/// </summary>
public class OkResponse : StatusCodeResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="OkResponse" />.
    /// </summary>
    public OkResponse() : base(StatusCodes.Status200OK) { }
}