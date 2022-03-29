using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 403 Forbidden response with a body.
/// </summary>
/// <typeparam name="TValue">The type of the value that will be serialized to the response body.</typeparam>
public sealed class ForbiddenObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenObjectResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public ForbiddenObjectResponse(TValue? value) : base(value, StatusCodes.Status403Forbidden) { }
}