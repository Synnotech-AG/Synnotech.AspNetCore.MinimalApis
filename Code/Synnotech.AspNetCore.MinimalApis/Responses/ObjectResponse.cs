using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synnotech.AspNetCore.MinimalApis.Responses.Http.Extensions;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an HTTP response that includes an object.
/// </summary>
public class ObjectResponse : IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="ObjectResponse" />.
    /// </summary>
    /// <param name="value">The object-value that should be set on the HTTP response.</param>
    public ObjectResponse(object? value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectResponse" />.
    /// </summary>
    /// <param name="value">The object-value that should be set on the HTTP response.</param>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    public ObjectResponse(object? value, int statusCode)
    {
        Value = value;
        StatusCode = statusCode.MustBeIn(StatusCodeRange);
    }

    /// <summary>
    /// Gets the object-value that will be set on the HTTP response.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Gets or sets the status code that will be on the HTTP response.
    /// </summary>
    public int? StatusCode { get; set; }

    private static Range<int> StatusCodeRange { get; } =
        Range.FromInclusive(100).ToInclusive(1000);

    /// <summary>
    /// Gets or sets the MIME ContentType that will be on the HTTP response.
    /// </summary>
    public string? ContentType { get; set; } // TODO: ContentType should be json by default

    /// <inheritdoc />
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when <paramref name="httpContext"/> is null.
    /// </exception>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.MustNotBeNull();
        if (Value is ProblemDetails problemDetails)
        {
            ApplyProblemDetailsDefaults(problemDetails);
        }

        if (StatusCode is { } statusCode)
        {
            httpContext.Response.StatusCode = statusCode;
        }

        ConfigureResponseHeaders(httpContext);

        if (Value is null)
        {
            return Task.CompletedTask;
        }

        OnFormatting(httpContext);
        return httpContext.Response.WriteAsJsonAsync(Value, Value.GetType(), options: null, contentType: ContentType);
    }

    /// <summary>
    /// Formats the HTTP response header.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> for the current request.</param>
    protected virtual void OnFormatting(HttpContext httpContext) { }

    /// <summary>
    /// Configures the HTTP response header.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> for the current request.</param>
    protected virtual void ConfigureResponseHeaders(HttpContext httpContext) { }

    private void ApplyProblemDetailsDefaults(ProblemDetails problemDetails)
    {
        if (problemDetails.Status is null)
        {
            if (StatusCode is not null)
            {
                problemDetails.Status = StatusCode;
            }
            else
            {
                problemDetails.Status = problemDetails is HttpValidationProblemDetails ?
                                            StatusCodes.Status400BadRequest :
                                            StatusCodes.Status500InternalServerError;
            }
        }

        StatusCode ??= problemDetails.Status;

        // ProblemDetailsDefaults is originally an internal class from Microsoft.AspNetCore.Http.Extensions
        if (ProblemDetailsDefaults.Defaults.TryGetValue(problemDetails.Status.Value, out var defaults))
        {
            problemDetails.Title ??= defaults.Title;
            problemDetails.Type ??= defaults.Type;
        }
    }
}