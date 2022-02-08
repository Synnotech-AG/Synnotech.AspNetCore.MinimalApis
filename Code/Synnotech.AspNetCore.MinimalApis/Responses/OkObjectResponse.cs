using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 200 OK response with body.
/// </summary>
public sealed class OkObjectResponse : ObjectResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="OkObjectResponse"/>.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    public OkObjectResponse(object? value) : base(value, StatusCodes.Status200OK)
    {
    }
}