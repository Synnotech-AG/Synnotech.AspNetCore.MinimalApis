using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 409 Conflict response.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public sealed class ConflictObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="ConflictObjectResponse{TValue}"/>.
    /// </summary>
    /// <param name="value">The object where the conflict happens.</param>
    public ConflictObjectResponse(TValue? value) : base(value, StatusCodes.Status409Conflict) { }
}