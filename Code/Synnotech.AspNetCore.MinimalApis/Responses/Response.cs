using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Provides factory methods that instantiate various HTTP responses.
/// </summary>
public static class Response
{
    /// <summary>
    /// Returns a response that only sets the specified HTTP status code.
    /// Please use the <see cref="StatusCodes" /> class to access all available status codes.
    /// </summary>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    public static StatusCodeResponse StatusCode(int statusCode) => new (statusCode);

    /// <summary>
    /// Returns a response with an HTTP 200 OK status code.
    /// </summary>
    public static OkResponse Ok() => new ();

    /// <summary>
    /// Returns a response with an HTTP 200 OK status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value that will be serialized to the response body.(optional)</param>
    public static OkObjectResponse<TValue> Ok<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Creates a response with an HTTP 201 Created status code.
    /// </summary>
    public static CreatedResponse Created() => new ();

    /// <summary>
    /// Creates a response with an HTTP 201 Created status code.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    public static CreatedResponse Created(string url) => new (url);

    /// <summary>
    /// Creates a response with an HTTP 201 Created status code.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    public static CreatedResponse Created(Uri url) => new (url);

    /// <summary>
    /// Creates a response with an HTTP 201 Created status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value that will be serialized to the response body.</param>
    public static CreatedObjectResponse<TValue> Created<TValue>(TValue value) => new (value);

    /// <summary>
    /// Returns a response with an HTTP 201 Created status code.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value that will be serialized to the response body.</param>
    /// <param name="url">The Url at which the content has been created.(optional)</param>
    public static CreatedObjectResponse<TValue> Created<TValue>(string url, TValue value) => new (url, value);

    /// <summary>
    /// Returns a response with an HTTP 201 Created status code.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value that will be serialized to the response body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    public static CreatedObjectResponse<TValue> Created<TValue>(Uri url, TValue value) => new (url, value);

    /// <summary>
    /// Creates a response with an HTTP 202 Accepted status code.
    /// </summary>
    public static AcceptedResponse Accepted() => new ();

    /// <summary>
    /// Creates a response with an HTTP 202 Accepted status code.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    public static AcceptedResponse Accepted(string url) => new (url);

    /// <summary>
    /// Creates a response with an HTTP 202 Accepted status code.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <param name="url">The URL that should be set as the "Location" header.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    public static AcceptedResponse Accepted(Uri url) => new (url);

    /// <summary>
    /// Returns a response with an HTTP 202 Accepted status code.
    /// </summary>
    /// <param name="value">The value that will be serialized to the response body.</param>
    public static AcceptedObjectResponse<TValue> Accepted<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response with an HTTP 202 Accepted status code with the specified body value.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value that will be serialized to the response body.(optional)</param>
    public static AcceptedObjectResponse<TValue> Accepted<TValue>(string url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response with an HTTP 202 Accepted status code with the specified body value.
    /// The url will be set as the "Location" header of the response.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    /// <param name="value">The value that will be serialized to the response body.(optional)</param>
    public static AcceptedObjectResponse<TValue> Accepted<TValue>(Uri url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response with an HTTP 204 No Content status code.
    /// </summary>
    public static NoContentResponse NoContent() => new ();

    /// <summary>
    /// Returns a response with an HTTP 307 Temporary Redirect status code.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <param name="preservedMethod">If set to true, make the temporary redirect preserve the initial request method.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="url" /> is empty.</exception>
    public static RedirectResponse RedirectTemporary(string url, bool preservedMethod) => new (url, permanent: false, preservedMethod);

    /// <summary>
    /// Returns a response with an HTTP 308 Permanent Redirect status code.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <param name="preservedMethod">If set to true, make the permanent redirect preserve the initial request method.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="url" /> is empty.</exception>
    public static RedirectResponse RedirectPermanent(string url, bool preservedMethod) => new (url, permanent: true, preservedMethod);

    /// <summary>
    /// Returns a response with an HTTP 400 Bad Request status code.
    /// </summary>
    public static BadRequestResponse BadRequest() => new ();

    /// <summary>
    /// Returns a response with an HTTP 400 Bad Request status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value that will be serialized to the response body.(optional)</param>
    public static BadRequestObjectResponse<TValue> BadRequest<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that is compliant with RFC-7807, with either an
    /// HTTP 400 or HTTP 500 status code by default. You can adjust the status code
    /// by using the optional <paramref name="statusCode" /> or by setting the status code
    /// directly on the <paramref name="problemDetails" /> instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="problemDetails" /> is null.</exception>
    public static ProblemDetailsResponse<T> ValidationProblem<T>(T problemDetails, int? statusCode = null)
        where T : ProblemDetails =>
        new (problemDetails, statusCode);

    /// <summary>
    /// Returns a response with an HTTP 401 Unauthorized status code.
    /// </summary>
    public static UnauthorizedResponse Unauthorized() => new ();

    /// <summary>
    /// Creates an HTTP 401 Unauthorized response with the specified value as the body.
    /// </summary>
    /// <param name="value">The value that should be serialized to the body of the HTTP response.</param>
    public static UnauthorizedObjectResponse<T> Unauthorized<T>(T value) => new (value);

    /// <summary>
    /// Returns either an HTTP 401 or an HTTP 403 response.
    /// The response will forward the result to the <see cref="IAuthenticationService" />
    /// registered with your DI container. The authentication service will then decide
    /// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
    /// will be returned.
    /// </summary>
    public static NotAllowedResponse NotAllowed() => new ();

    /// <summary>
    /// Returns either an HTTP 401 or an HTTP 403 response.
    /// The response will forward the result to the <see cref="IAuthenticationService" />
    /// registered with your DI container. The authentication service will then decide
    /// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
    /// will be returned.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    public static NotAllowedResponse NotAllowed(string authenticationScheme) => new (authenticationScheme);

    /// <summary>
    /// Returns either an HTTP 401 or an HTTP 403 response.
    /// The response will forward the result to the <see cref="IAuthenticationService" />
    /// registered with your DI container. The authentication service will then decide
    /// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
    /// will be returned.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
    public static NotAllowedResponse NotAllowed(IList<string> authenticationSchemes) => new (authenticationSchemes);

    /// <summary>
    /// Returns either an HTTP 401 or an HTTP 403 response.
    /// The response will forward the result to the <see cref="IAuthenticationService" />
    /// registered with your DI container. The authentication service will then decide
    /// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
    /// will be returned.
    /// </summary>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.(optional)</param>
    public static NotAllowedResponse NotAllowed(AuthenticationProperties? properties) => new (properties);

    /// <summary>
    /// Returns either an HTTP 401 or an HTTP 403 response.
    /// The response will forward the result to the <see cref="IAuthenticationService" />
    /// registered with your DI container. The authentication service will then decide
    /// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
    /// will be returned.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.(optional)</param>
    public static NotAllowedResponse NotAllowed(string authenticationScheme, AuthenticationProperties? properties) => new (authenticationScheme, properties);

    /// <summary>
    /// Returns either an HTTP 401 or an HTTP 403 response.
    /// The response will forward the result to the <see cref="IAuthenticationService" />
    /// registered with your DI container. The authentication service will then decide
    /// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
    /// will be returned.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.(optional)</param>
    public static NotAllowedResponse NotAllowed(IList<string> authenticationSchemes, AuthenticationProperties? properties) => new (authenticationSchemes, properties);

    /// <summary>
    /// Creates an HTTP 403 Forbidden response.
    /// </summary>
    public static ForbiddenResponse Forbidden() => new ();

    /// <summary>
    /// Creates an HTTP 403 Forbidden response with a body.
    /// </summary>
    /// <param name="value">The value that will be serialized to the response body.</param>
    public static ForbiddenObjectResponse<TValue> Forbidden<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response with an HTTP 404 Not Found status code.
    /// </summary>
    public static NotFoundResponse NotFound() => new ();

    /// <summary>
    /// Returns a response with an HTTP 409 Conflict status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The object where the conflict happens.(optional)</param>
    public static ConflictObjectResponse<TValue> Conflict<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response with an HTTP 500 Internal Server Error code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value that will be serialized to the response body.(optional)</param>
    public static InternalServerErrorResponse<TValue> InternalServerError<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that includes a string as content.
    /// Writes the <paramref name="content" /> string to the HTTP response.
    /// </summary>
    /// <param name="content">The content to write to the response.</param>
    /// <param name="contentType">The content type (MIME type).</param>
    public static ContentResponse Content(string? content, MediaTypeHeaderValue contentType) => new (content, contentType.ToString());

    /// <summary>
    /// Returns a response that includes a string as content.
    /// Writes the <paramref name="content" /> string to the HTTP response.
    /// </summary>
    /// <param name="content">The content to write to the response.</param>
    /// <param name="contentType">The content type (MIME type).</param>
    /// <param name="contentEncoding">The content encoding.</param>
    public static ContentResponse Content(string? content, string? contentType = null, Encoding? contentEncoding = null)
    {
        MediaTypeHeaderValue? mediaTypeHeaderValue = null;
        if (contentType is not null)
        {
            mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);
            mediaTypeHeaderValue.Encoding = contentEncoding ?? mediaTypeHeaderValue.Encoding;
        }

        return new (content, mediaTypeHeaderValue?.ToString());
    }

    /// <summary>
    /// Returns a response that includes a string as content.
    /// Writes the <paramref name="content" /> string to the HTTP response.
    /// </summary>
    /// <param name="content">The content to write to the response.</param>
    /// <param name="statusCode">The status code sent in the HTTP response.</param>
    /// <param name="contentEncoding">The content encoding.</param>
    /// <param name="contentType">The content type (MIME type).</param>
    public static ContentResponse Content(string? content, int statusCode, Encoding? contentEncoding = null, string? contentType = null)
    {
        MediaTypeHeaderValue? mediaTypeHeaderValue = null;
        if (contentType is not null)
        {
            mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);
            mediaTypeHeaderValue.Encoding = contentEncoding ?? mediaTypeHeaderValue.Encoding;
        }

        return new (content, mediaTypeHeaderValue?.ToString(), statusCode);
    }

    /// <summary>
    /// Returns a response that provides a FileStream.
    /// </summary>
    /// <param name="fileStream">The stream with the file.</param>
    /// <param name="contentType">The Content-Type header of the response.(optional)</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.(optional)</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.(optional)
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.(optional)
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.(optional)</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileStream" /> is null.</exception>
    public static StreamResponse Stream(Stream fileStream,
                                        string? contentType = null,
                                        string? fileDownloadName = null,
                                        DateTimeOffset? lastModified = null,
                                        EntityTagHeaderValue? entityTag = null,
                                        bool enableRangeProcessing = false) =>
        new (fileStream, contentType)
        {
            LastModified = lastModified,
            EntityTag = entityTag,
            FileDownloadName = fileDownloadName,
            EnableRangeProcessing = enableRangeProcessing
        };

    /// <summary>
    /// Returns a response that provides a FileStream.
    /// </summary>
    /// <param name="fileStream">The stream with the file.</param>
    /// <param name="contentType">The Content-Type header of the response.(optional)</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.(optional)</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.(optional)
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.(optional)
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.(optional)</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileStream" /> is null.</exception>
    public static FileResponse File(Stream fileStream,
                                    string? contentType = null,
                                    string? fileDownloadName = null,
                                    DateTimeOffset? lastModified = null,
                                    EntityTagHeaderValue? entityTag = null,
                                    bool enableRangeProcessing = false) =>
        new StreamResponse(fileStream, contentType)
        {
            LastModified = lastModified,
            EntityTag = entityTag,
            FileDownloadName = fileDownloadName,
            EnableRangeProcessing = enableRangeProcessing
        };

    /// <summary>
    /// Returns a response that provides a ByteArray file.
    /// </summary>
    /// <param name="fileContents">The bytes that represent the file content.</param>
    /// <param name="contentType">The Content-Type header of the response.(optional)</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.(optional)</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.(optional)
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.(optional)
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.(optional)</param>
    public static ByteArrayResponse ByteArray(ReadOnlyMemory<byte> fileContents,
                                              string? contentType = null,
                                              string? fileDownloadName = null,
                                              DateTimeOffset? lastModified = null,
                                              EntityTagHeaderValue? entityTag = null,
                                              bool enableRangeProcessing = false) =>
        new (fileContents, contentType)
        {
            LastModified = lastModified,
            EntityTag = entityTag,
            FileDownloadName = fileDownloadName,
            EnableRangeProcessing = enableRangeProcessing
        };

    /// <summary>
    /// Returns a response that provides a ByteArray file.
    /// </summary>
    /// <param name="fileContents">The bytes that represent the file content.</param>
    /// <param name="contentType">The Content-Type header of the response.(optional)</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.(optional)</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.(optional)
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.(optional)
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.(optional)</param>
    public static FileResponse File(ReadOnlyMemory<byte> fileContents,
                                    string? contentType = null,
                                    string? fileDownloadName = null,
                                    DateTimeOffset? lastModified = null,
                                    EntityTagHeaderValue? entityTag = null,
                                    bool enableRangeProcessing = false) =>
        new ByteArrayResponse(fileContents, contentType)
        {
            LastModified = lastModified,
            EntityTag = entityTag,
            FileDownloadName = fileDownloadName,
            EnableRangeProcessing = enableRangeProcessing
        };

    /// <summary>
    /// Returns a response that provides the file at the specified <paramref name="filePath" />.
    /// </summary>
    /// <param name="filePath">The path to the file. The path must be an absolute path.</param>
    /// <param name="contentType">The Content-Type header of the response.(optional)</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.(optional)</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.(optional)
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.(optional)
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.(optional)</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="filePath" /> is null.</exception>
    public static FileResponse File(string filePath,
                                    string? contentType = null,
                                    string? fileDownloadName = null,
                                    DateTimeOffset? lastModified = null,
                                    EntityTagHeaderValue? entityTag = null,
                                    bool enableRangeProcessing = false)
    {
        if (Path.IsPathRooted(filePath))
        {
            return new PhysicalFileResponse(filePath, contentType)
            {
                LastModified = lastModified,
                EntityTag = entityTag,
                FileDownloadName = fileDownloadName,
                EnableRangeProcessing = enableRangeProcessing
            };
        }

        return new VirtualFileResponse(filePath, contentType)
        {
            LastModified = lastModified,
            EntityTag = entityTag,
            FileDownloadName = fileDownloadName,
            EnableRangeProcessing = enableRangeProcessing
        };
    }
}