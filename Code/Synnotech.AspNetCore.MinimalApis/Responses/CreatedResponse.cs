using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 201 Created response.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public sealed class CreatedResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreatedResponse{TValue}" /> with values provided.
    /// </summary>
    /// <param name="url">The Url at which the content has been created.</param>
    /// <param name="value">The value to format in the entity body.</param>
    public CreatedResponse(TValue value, string? url = default) : base(value.MustNotBeNullReference(), StatusCodes.Status201Created) =>
        Url = url;

    /// <summary>
    /// Initializes a new instance of <see cref="CreatedResponse{TValue}" /> with values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public CreatedResponse(TValue value, Uri url) : base(value.MustNotBeNullReference(), StatusCodes.Status201Created) =>
        Url = url.PrepareForLocationHeader();

    /// <summary>
    /// Gets the Url at which the created content can be found.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Sets the "Location" header on the HTTP response if a URL was passed to this response.
    /// </summary>
    protected override void ConfigureResponse(HttpContext httpContext)
    {
        if (!Url.IsNullOrWhiteSpace())
            httpContext.Response.Headers.Location = Url;
    }
}