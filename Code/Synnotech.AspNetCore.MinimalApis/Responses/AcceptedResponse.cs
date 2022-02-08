using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 202 Accepted response.
/// </summary>
public sealed class AcceptedResponse : ObjectResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedResponse" /> class with the values provided.
    /// </summary>
    public AcceptedResponse() : base(value: null, StatusCodes.Status202Accepted) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedResponse" /> class with the values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    public AcceptedResponse(string? url, object? value) : base(value, StatusCodes.Status202Accepted)
    {
        Url = url;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptedResponse" /> class with the values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    public AcceptedResponse(Uri? url, object? value) : base(value, StatusCodes.Status201Created)
    {
        url.MustNotBeNull();

        if (url.IsAbsoluteUri)
            Url = url.AbsoluteUri;
        else
            Url = url.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
    }

    /// <summary>
    /// Gets the Url at which the created content can be found.
    /// </summary>
    public string? Url { get; }


    /// <inheritdoc />
    protected override void ConfigureResponseHeaders(HttpContext httpContext)
    {
        if(!string.IsNullOrEmpty(Url))
            httpContext.Response.Headers.Location = Url;
    }
}