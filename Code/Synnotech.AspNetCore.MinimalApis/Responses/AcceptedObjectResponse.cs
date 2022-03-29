using System;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 202 Accepted response with a body.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public class AcceptedObjectResponse<TValue> : ObjectResponseWithLocation<TValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedObjectResponse{TValue}" /> class.
    /// </summary>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public AcceptedObjectResponse(TValue? value) : base(value, StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedObjectResponse{TValue}" /> class.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    public AcceptedObjectResponse(string url, TValue? value) : base(url, value, StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedObjectResponse{TValue}" /> class.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    public AcceptedObjectResponse(Uri url, TValue? value) : base(url, value, StatusCodes.Status202Accepted) { }
}