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
    public static OkObjectResponse<TValue> OkObject<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 201 Created status code.
    /// </summary>
    public static CreatedResponse<TValue> CreatedWithString<TValue>(string? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 201 Created status code.
    /// </summary>
    public static CreatedResponse<TValue> CreatedWithUri<TValue>(Uri? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    public static AcceptedResponse<TValue> Accepted<TValue>(TValue? value) => new (value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    public static AcceptedResponse<TValue> AcceptedWithString<TValue>(string? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 202 Accepted status code.
    /// </summary>
    public static AcceptedResponse<TValue> AcceptedWithUri<TValue>(Uri? url, TValue? value) => new (url, value);

    /// <summary>
    /// Returns a response that sets the HTTP 204 No Content status code.
    /// </summary>
    public static NoContentResponse NoContent() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 400 Bad Request status code.
    /// </summary>
    public static BadRequestResponse BadRequest() => new ();

    /// <summary>
    /// Returns a response that sets the HTTP 400 Bad Request status code.
    /// </summary>
    public static BadRequestObjectResponse<TValue> BadRequestObject<TValue>(TValue? value) => new (value);
    
    /// <summary>
    /// Returns a response that sets the HTTP 404 Not Found status code.
    /// </summary>
    public static NotFoundResponse NotFound() => new();
}