using System;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an object response, having a status code, a body, and a "Location" header.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public abstract class ObjectResponseWithLocation<TValue> : ObjectResponse<TValue>, IHasLocationUrl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectResponseWithLocation{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body (optional).</param>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    protected ObjectResponseWithLocation(TValue? value, int statusCode) : base(value, statusCode) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectResponseWithLocation{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    protected ObjectResponseWithLocation(string url, TValue? value, int statusCode) : base(value, statusCode) =>
        Url = url;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectResponseWithLocation{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    protected ObjectResponseWithLocation(Uri url, TValue? value, int statusCode) : base(value, statusCode) =>
        Url = url.PrepareForLocationHeader();

    /// <summary>
    /// Gets the Url at which the created content can be found.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Sets the "Location" header on the HTTP response if a URL was passed to this response.
    /// </summary>
    protected override void ConfigureResponse(HttpContext httpContext) =>
        httpContext.TrySetLocationHeader(Url);
}