using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 200 OK response with body.
/// </summary>
/// <typeparam name="TValue">The type of the object in the body.</typeparam>
public class OkObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="OkObjectResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public OkObjectResponse(TValue? value) : base(value, StatusCodes.Status200OK) { }
}