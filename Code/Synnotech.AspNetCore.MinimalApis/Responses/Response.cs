using System;
using Microsoft.AspNetCore.Http;

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
    public static StatusCodeResponse StatusCode(int statusCode) =>
        new (statusCode);

    /// <summary>
    /// Returns a response that sets the HTTP 200 OK status code.
    /// </summary>
    public static OkResponse Ok() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 200 OK status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    public static OkObjectResponse<TValue> OkObject<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 201 Created status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public static CreatedResponse<TValue> CreatedWithString<TValue>(string? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 201 Created status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the content has been created.</param>
    public static CreatedResponse<TValue> CreatedWithUri<TValue>(Uri? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    public static AcceptedResponse<TValue> Accepted<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    public static AcceptedResponse<TValue> AcceptedWithString<TValue>(string? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    /// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
    /// <param name="value">The value to format in the entity body.</param>
    /// <param name="url">The Url at which the status of requested content can be monitored.</param>
    public static AcceptedResponse<TValue> AcceptedWithUri<TValue>(Uri? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 204 No Content status code.
    /// </summary>
    public static NoContentResponse NoContent() => new ();


    /// <summary>
    /// Returns a response that sets the HTTP 307 Temporary Redirect status code.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <param name="preservedMethod">If set to true, make the temporary redirect preserve the initial request method.</param>
    public static RedirectResponse RedirectTemporary(string url, bool preservedMethod) => new(url, permanent: false, preservedMethod);


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
    public static BadRequestObjectResponse<TValue> BadRequestObject<TValue>(TValue? value) => new (value);
    
    /// <summary>
    /// Returns a response that sets the HTTP 404 Not Found status code.
    /// </summary>
    public static NotFoundResponse NotFound() => new();

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
}