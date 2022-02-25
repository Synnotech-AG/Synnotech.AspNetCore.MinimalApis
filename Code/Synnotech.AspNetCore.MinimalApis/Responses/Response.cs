using System;
using System.Collections.Generic;
using System.IO;
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
    /// Returns a response that sets the HTTP 200 OK status code.
    /// </summary>
    public static OkResponse Ok() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 200 OK status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    public static OkObjectResponse<TValue> Ok<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 201 Created status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public static CreatedResponse<TValue> Created<TValue>(TValue value, string? url = default) => new (value, url);

    /// <summary>
    /// Returns a response that sets the HTTP 201 Created status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public static CreatedResponse<TValue> Created<TValue>(TValue value, Uri url) => new (value, url);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    public static AcceptedResponse<TValue> Accepted<TValue>(TValue? value = default) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    public static AcceptedResponse<TValue> Accepted<TValue>(string url, TValue? value = default) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    public static AcceptedResponse<TValue> Accepted<TValue>(Uri url, TValue? value = default) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 204 No Content status code.
    /// </summary>
    public static NoContentResponse NoContent() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 307 Temporary Redirect status code.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <param name="preservedMethod">If set to true, make the temporary redirect preserve the initial request method.</param>
    public static RedirectResponse RedirectTemporary(string url, bool preservedMethod) => new (url, permanent: false, preservedMethod);

    /// <summary>
    /// Returns a response that sets the HTTP 308 Permanent Redirect status code.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <param name="preservedMethod">If set to true, make the permanent redirect preserve the initial request method.</param>
    public static RedirectResponse RedirectPermanent(string url, bool preservedMethod) => new (url, permanent: true, preservedMethod);

    /// <summary>
    /// Returns a response that sets the HTTP 400 Bad Request status code.
    /// </summary>
    public static BadRequestResponse BadRequest() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 400 Bad Request status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    public static BadRequestObjectResponse<TValue> BadRequest<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that is compliant with RFC-7807, with either an
    /// HTTP 400 or HTTP 500 status code by default. You can adjust the status code
    /// by using the optional <paramref name="statusCode" /> or by setting the status code
    /// directly on the <paramref name="problemDetails" /> instance.
    /// </summary>
    public static ProblemDetailsResponse<T> ValidationProblem<T>(T problemDetails, int? statusCode = null)
        where T : ProblemDetails =>
        new (problemDetails, statusCode);

    /// <summary>
    /// Returns a response that sets the HTTP 401 Unauthorized status code.
    /// </summary>
    /// <returns></returns>
    public static UnauthorizedResponse Unauthorized() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 403 Forbidden status code.
    /// </summary>
    public static ForbiddenResponse Forbidden() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 403 Forbidden status code.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    public static ForbiddenResponse Forbidden(string authenticationScheme) => new (authenticationScheme);

    /// <summary>
    /// Returns a response that sets the HTTP 403 Forbidden status code.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
    public static ForbiddenResponse Forbidden(IList<string> authenticationSchemes) => new (authenticationSchemes);

    /// <summary>
    /// Returns a response that sets the HTTP 403 Forbidden status code.
    /// </summary>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public static ForbiddenResponse Forbidden(AuthenticationProperties? properties) => new (properties);

    /// <summary>
    /// Returns a response that sets the HTTP 403 Forbidden status code.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public static ForbiddenResponse Forbidden(string authenticationScheme, AuthenticationProperties? properties) => new (authenticationScheme, properties);

    /// <summary>
    /// Returns a response that sets the HTTP 403 Forbidden status code.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public static ForbiddenResponse Forbidden(IList<string> authenticationSchemes, AuthenticationProperties? properties) => new (authenticationSchemes, properties);

    /// <summary>
    /// Returns a response that sets the HTTP 404 Not Found status code.
    /// </summary>
    public static NotFoundResponse NotFound() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 409 Conflict status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The object where the conflict happens.</param>
    public static ConflictObjectResponse<TValue> Conflict<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 500 Internal Server Error code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    public static InternalServerErrorResponse<TValue> InternalServerError<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that provides a FileStream.
    /// </summary>
    /// <param name="fileStream">The stream with the file.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.</param>
    public static StreamResponse Stream(
        Stream fileStream,
        string? contentType = null,
        string? fileDownloadName = null,
        DateTimeOffset? lastModified = null,
        EntityTagHeaderValue? entityTag = null,
        bool enableRangeProcessing = false
    ) => new (fileStream, contentType)
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
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.</param>
    public static StreamResponse File(
        Stream fileStream,
        string? contentType = null,
        string? fileDownloadName = null,
        DateTimeOffset? lastModified = null,
        EntityTagHeaderValue? entityTag = null,
        bool enableRangeProcessing = false
    ) => new(fileStream, contentType)
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
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.</param>
    public static ByteArrayResponse ByteArray(
        ReadOnlyMemory<byte> fileContents,
        string? contentType = null,
        string? fileDownloadName = null,
        DateTimeOffset? lastModified = null,
        EntityTagHeaderValue? entityTag = null,
        bool enableRangeProcessing = false
    ) => new (fileContents, contentType)
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
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.</param>
    public static ByteArrayResponse File(
        ReadOnlyMemory<byte> fileContents,
        string? contentType = null,
        string? fileDownloadName = null,
        DateTimeOffset? lastModified = null,
        EntityTagHeaderValue? entityTag = null,
        bool enableRangeProcessing = false
    ) => new(fileContents, contentType)
    {
        LastModified = lastModified,
        EntityTag = entityTag,
        FileDownloadName = fileDownloadName,
        EnableRangeProcessing = enableRangeProcessing
    };

    /// <summary>
    /// Returns a response that provides a physical file from the disk.
    /// </summary>
    /// <param name="filePath">The path to the file. The path must be an absolute path.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.</param>
    public static PhysicalFileResponse PhysicalFile(
        string filePath,
        string? contentType = null,
        string? fileDownloadName = null,
        DateTimeOffset? lastModified = null,
        EntityTagHeaderValue? entityTag = null,
        bool enableRangeProcessing = false
    ) => new (filePath, contentType)
    {
        LastModified = lastModified,
        EntityTag = entityTag,
        FileDownloadName = fileDownloadName,
        EnableRangeProcessing = enableRangeProcessing
    };

    /// <summary>
    /// Returns a response that provides a physical file from the disk.
    /// </summary>
    /// <param name="filePath">The path to the file. The path must be an absolute path.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <param name="fileDownloadName">The file name to be used in the <c>Content-Disposition</c> header.</param>
    /// <param name="lastModified">
    /// The <see cref="DateTimeOffset" /> of when the file was last modified.
    /// Used to configure the <c>Last-Modified</c> response header and perform conditional range requests.
    /// </param>
    /// <param name="entityTag">
    /// The <see cref="EntityTagHeaderValue" /> to be configure the <c>ETag</c> response header
    /// and perform conditional requests.
    /// </param>
    /// <param name="enableRangeProcessing">Set to <c>true</c> to enable range requests processing.</param>
    public static PhysicalFileResponse File(
        string filePath,
        string? contentType = null,
        string? fileDownloadName = null,
        DateTimeOffset? lastModified = null,
        EntityTagHeaderValue? entityTag = null,
        bool enableRangeProcessing = false
    ) => new (filePath, contentType)
    {
        LastModified = lastModified,
        EntityTag = entityTag,
        FileDownloadName = fileDownloadName,
        EnableRangeProcessing = enableRangeProcessing
    };
}