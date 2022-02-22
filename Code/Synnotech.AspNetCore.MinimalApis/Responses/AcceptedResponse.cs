using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Internals;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 202 Accepted response.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public sealed class AcceptedResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedResponse{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body (optional).</param>
    public AcceptedResponse(TValue? value = default) : base(value, StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedResponse{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    public AcceptedResponse(string url, TValue? value = default) : base(value, StatusCodes.Status202Accepted) =>
        Url = url;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedResponse{TValue}" /> class with the values provided.
    /// </summary>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value to format in the entity body (optional).</param>
    public AcceptedResponse(Uri url, TValue? value = default) : base(value, StatusCodes.Status202Accepted)
    {
        Url = url.PrepareForLocationHeader();
    }

    /// <summary>
    /// Gets the Url at which the created content can be found.
    /// </summary>
    public string? Url { get; }


    /// <inheritdoc />
    protected override void ConfigureResponse(HttpContext httpContext)
    {
        if(!Url.IsNullOrEmpty())
            httpContext.Response.Headers.Location = Url;
    }
}