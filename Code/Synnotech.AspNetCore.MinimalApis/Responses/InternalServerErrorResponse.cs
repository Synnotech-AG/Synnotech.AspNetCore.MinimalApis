using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 500 Internal Server Error response.
/// </summary>
/// <typeparam name="TValue">The type of the object in the body.</typeparam>
public sealed class InternalServerErrorResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="InternalServerErrorResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    public InternalServerErrorResponse(TValue? value) : base(value, StatusCodes.Status500InternalServerError) { }
}