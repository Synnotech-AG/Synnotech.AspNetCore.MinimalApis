using System.Text.Json;
using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an HTTP response that includes an object.
/// </summary>
/// <typeparam name="TValue">The type of the HTTP response body object.</typeparam>
public class ObjectResponse<TValue> : IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="ObjectResponse{TValue}" />.
    /// </summary>
    /// <param name="value">The object-value that should be set on the HTTP response.</param>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    public ObjectResponse(TValue? value, int statusCode)
    {
        Value = value;
        StatusCode = statusCode.MustBeIn(StatusCodeResponse.StatusCodeRange);
    }

    /// <summary>
    /// Gets the value that will be set on the HTTP response. This value might be null.
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// Gets or sets the status code that will be on the HTTP response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the MIME ContentType that will be on the HTTP response.
    /// </summary>
    public string? ContentType { get; set; } = "application/json";

    /// <summary>
    /// Gets or sets the options that are used to serialize the value.
    /// The default value is null.
    /// </summary>
    public JsonSerializerOptions? JsonSerializerOptions { get; set; }

    /// <inheritdoc />
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when <paramref name="httpContext" /> is null.
    /// </exception>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.MustNotBeNull();
        httpContext.Response.StatusCode = StatusCode;

        ConfigureResponse(httpContext);

        return Value is null ?
                   Task.CompletedTask :
                   httpContext.Response.WriteAsJsonAsync(Value, JsonSerializerOptions, ContentType);
    }

    /// <summary>
    /// Configures the HTTP response header.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext" /> for the current request.</param>
    protected virtual void ConfigureResponse(HttpContext httpContext) { }
}

