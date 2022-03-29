using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 401 Unauthorized response with a body.
/// </summary>
/// <typeparam name="TValue">The type of the value that will be serialized to the response body.</typeparam>
public class UnauthorizedObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="UnauthorizedObjectResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public UnauthorizedObjectResponse(TValue? value) : base(value, StatusCodes.Status401Unauthorized) { }
}