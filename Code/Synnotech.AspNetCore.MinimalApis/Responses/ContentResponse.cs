using System;
using System.Text;
using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an HTTP response that includes a string.
/// </summary>
public class ContentResponse : IResult
{
    private const string DefaultContentType = "text/plain; charset=utf-8";
    private static readonly Encoding DefaultEncoding = Encoding.UTF8;

    /// <summary>
    /// Initializes a new instance of <see cref="ContentResponse" />.
    /// </summary>
    /// <param name="content">The content that should be sent in the HTTP response.</param>
    /// <param name="contentType">
    /// The contentType of the sent content. By default set to <c>"text/plain; charset=utf-8"</c>.
    /// </param>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when either <paramref name="content" /> is null or empty.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    public ContentResponse(string? content, string? contentType, int statusCode) : this(content, contentType)
    {
        StatusCode = statusCode.MustBeIn(StatusCodeResponse.StatusCodeRange);
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ContentResponse" />.
    /// </summary>
    /// <param name="content">The content that should be sent in the HTTP response.</param>
    /// <param name="contentType">
    /// The contentType of the sent content. By default set to <c>"text/plain; charset=utf-8"</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when either <paramref name="content" /> is null or empty.
    /// </exception>
    public ContentResponse(string? content, string? contentType)
    {
        Content = content.MustNotBeNullOrEmpty();
        ContentType = contentType;
    }

    /// <summary>
    /// Gets the content representing the body of the response.
    /// </summary>
    public string Content { get; }

    /// <summary>
    /// Gets the Content-Type header for the response.
    /// </summary>
    public string? ContentType { get; }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public int? StatusCode { get; }

    /// <inheritdoc />
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when <paramref name="httpContext" /> is null.
    /// </exception>
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.MustNotBeNull();

        var response = httpContext.Response;

        ResponseContentTypeHelper.ResolveContentTypeAndEncoding(
            ContentType,
            response.ContentType,
            (DefaultContentType, DefaultEncoding),
            ResponseContentTypeHelper.GetEncoding,
            out var resolvedContentType,
            out var resolvedContentTypeEncoding
        );

        response.ContentType = resolvedContentType;

        if (StatusCode != null)
        {
            response.StatusCode = StatusCode.Value;
        }

        response.ContentLength = resolvedContentTypeEncoding.GetByteCount(Content);
        await response.WriteAsync(Content, resolvedContentTypeEncoding);
    }
}