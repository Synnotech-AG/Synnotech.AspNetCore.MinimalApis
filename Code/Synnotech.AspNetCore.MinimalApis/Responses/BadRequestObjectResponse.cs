using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 400 Bad Request response with body.
/// </summary>
public sealed class BadRequestObjectResponse : ObjectResponse
{
    /// <summary>
    /// Initializes a new instance of <see cref="BadRequestObjectResponse"/> with values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    public BadRequestObjectResponse(object? value) : base(value, StatusCodes.Status400BadRequest) { }
}