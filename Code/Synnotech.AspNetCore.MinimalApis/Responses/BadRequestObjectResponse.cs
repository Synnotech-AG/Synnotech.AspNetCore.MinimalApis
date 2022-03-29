using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 400 Bad Request response with body.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public class BadRequestObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="BadRequestObjectResponse{TValue}" /> with values provided.
    /// </summary>
    /// <param name="value">The value that will be serialized to the response body.</param>
    public BadRequestObjectResponse(TValue? value) : base(value, StatusCodes.Status400BadRequest) { }
}