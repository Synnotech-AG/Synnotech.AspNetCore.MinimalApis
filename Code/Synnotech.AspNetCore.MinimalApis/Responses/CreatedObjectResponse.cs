using System;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 201 Created response with a body.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public class CreatedObjectResponse<TValue> : ObjectResponseWithLocation<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreatedObjectResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public CreatedObjectResponse(TValue? value) : base(value, StatusCodes.Status201Created) { }

    /// <summary>
    /// Initializes a new instance of <see cref="CreatedObjectResponse{TValue}" /> with values provided.
    /// </summary>
    /// <param name="url">The Url at which the content has been created.</param>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public CreatedObjectResponse(string url, TValue value) : base(url, value, StatusCodes.Status201Created) { }

    /// <summary>
    /// Initializes a new instance of <see cref="CreatedObjectResponse{TValue}" /> with values provided.
    /// </summary>
    /// <param name="url">The Url at which the content has been created.</param>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    public CreatedObjectResponse(Uri url, TValue value) : base(url, value, StatusCodes.Status201Created) { }
}