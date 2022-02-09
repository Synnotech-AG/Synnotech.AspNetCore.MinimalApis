using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 201 Created response.
/// </summary>
public sealed class CreatedResponse<TValue> : ObjectResponse<TValue>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreatedResponse" /> with values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public CreatedResponse(string? url, TValue? value) : base(value, StatusCodes.Status201Created)
    {
        Url = url;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="CreatedResponse" /> with values provided.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public CreatedResponse(Uri? url, TValue? value) : base(value, StatusCodes.Status201Created)
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
        httpContext.Response.Headers.Location = Url;
    }
}