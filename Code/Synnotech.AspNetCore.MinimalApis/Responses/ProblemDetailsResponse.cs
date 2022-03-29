using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synnotech.AspNetCore.MinimalApis.Responses.Tools;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents an object response that either returns HTTP 400 Bad Request or
/// HTTP 500 Internal Server Error depending on the provided <see cref="ProblemDetails" />
/// instance. The response is compliant to RFC-7807.
/// </summary>
/// <typeparam name="T">The type of the problem details. Must be or derive from <see cref="ProblemDetails" />.</typeparam>
public sealed class ProblemDetailsResponse<T> : ObjectResponse<T>
    where T : ProblemDetails
{
    /// <summary>
    /// Initializes a new instance of <see cref="ProblemDetailsResponse{T}" />.
    /// The content type is set to "application/problem+json" by default.
    /// </summary>
    /// <param name="problemDetails">The problem details instance that describes the error.</param>
    /// <param name="statusCode">
    /// The status code that should be used for the response (optional). A default code will
    /// be determined automatically when no value is supplied, either via this parameter or
    /// directly on the <paramref name="problemDetails" /> instance.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="problemDetails" /> is null.</exception>
    public ProblemDetailsResponse(T problemDetails,
                                  int? statusCode = null)
        : base(problemDetails.ApplyProblemDetailsDefaults(statusCode),
               problemDetails.Status!.Value)
    {
        ContentType = "application/problem+json";
    }
}

/// <summary>
/// Provides extension methods for <see cref="ProblemDetails" />.
/// </summary>
public static class ProblemDetailsExtensions
{
    /// <summary>
    /// Applies the default values to the problem details instance. If no status code is set on the
    /// <paramref name="problemDetails" /> instance, either the supplied <paramref name="statusCode" />
    /// or an automatically determined status code is used (when <see cref="HttpValidationProblemDetails" />
    /// is passed => HTTP 400, otherwise HTTP 500). Also, the title and the type are set if they are not already present
    /// (see <see cref="ProblemDetailsDefaults" /> for details).
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="problemDetails" /> is null.</exception>
    public static T ApplyProblemDetailsDefaults<T>(this T problemDetails, int? statusCode = null)
        where T : ProblemDetails
    {
        problemDetails.MustNotBeNull();

        if (problemDetails.Status is null)
        {
            if (statusCode.HasValue)
            {
                problemDetails.Status = statusCode.Value;
            }
            else
            {
                problemDetails.Status = problemDetails is HttpValidationProblemDetails ?
                                            StatusCodes.Status400BadRequest :
                                            StatusCodes.Status500InternalServerError;
            }
        }

        if (ProblemDetailsDefaults.Defaults.TryGetValue(problemDetails.Status.Value, out var defaults))
        {
            problemDetails.Title ??= defaults.Title;
            problemDetails.Type ??= defaults.Type;
        }

        return problemDetails;
    }
}