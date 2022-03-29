using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 500 Internal Server Error response with a body.
/// </summary>
/// <typeparam name="TValue">The type of the object in the body.</typeparam>
public class InternalServerErrorObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="InternalServerErrorObjectResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The value that will be serialized to the response body.</param>
    public InternalServerErrorObjectResponse(TValue? value) : base(value, StatusCodes.Status500InternalServerError) { }
}