using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 202 Accepted response with a body.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public class AcceptedObjectResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedObjectResponse{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body (optional).</param>
    public AcceptedObjectResponse(TValue? value) : base(value, StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedObjectResponse{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    public AcceptedObjectResponse(string url, TValue? value) : base(value, StatusCodes.Status202Accepted) =>
        Url = url;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedObjectResponse{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
    public AcceptedObjectResponse(Uri url, TValue? value) : base(value, StatusCodes.Status202Accepted) =>
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