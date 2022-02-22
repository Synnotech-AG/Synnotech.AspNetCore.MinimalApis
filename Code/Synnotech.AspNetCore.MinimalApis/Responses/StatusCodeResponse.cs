using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an HTTP response that only sets the status code.
/// </summary>
public class StatusCodeResponse : IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="StatusCodeResponse" />.
    /// </summary>
    /// <param name="statusCode">The status code that should be set on the HTTP response.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="statusCode" /> is not between 100 and 1000 (both values inclusive).
    /// </exception>
    public StatusCodeResponse(int statusCode) =>
        StatusCode = statusCode.MustBeIn(StatusCodeRange);

    /// <summary>
    /// Gets the range of allowed status codes (100 to 1000, both inclusive).
    /// </summary>
    public static Range<int> StatusCodeRange { get; } =
        Range.FromInclusive(100).ToInclusive(1000);

    /// <summary>
    /// Gets the status code that will be set on the HTTP response.
    /// </summary>
    public int StatusCode { get; }

    /// <inheritdoc />
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when <paramref name="httpContext"/> is null.
    /// </exception>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.MustNotBeNull()
                   .Response
                   .StatusCode = StatusCode;
        return Task.CompletedTask;
    }
}