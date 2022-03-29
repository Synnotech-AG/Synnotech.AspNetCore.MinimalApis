using System;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Provides extension methods for <see cref="IResult" /> instances.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Tries to retrieve the status code from the specified result.
    /// The result instance must implement the <see cref="IHasStatusCode" /> interface,
    /// or otherwise an <see cref="InvalidOperationException" /> will be thrown.
    /// </summary>
    /// <param name="result">The result whose status code should be retrieved.</param>
    /// <exception cref="InvalidOperationException">
    /// Throw when <paramref name="result" /> cannot be cast to <see cref="IHasStatusCode" />
    /// and thus no status code retrieval is possible.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="result" /> is null.</exception>
    public static int GetStatusCode(this IResult result)
    {
        result.MustNotBeNull();

        if (result is IHasStatusCode response)
            return response.StatusCode;

        throw new InvalidOperationException($"The result instance {result} cannot be cast to interface IHasStatusCode");
    }

    /// <summary>
    /// Tries to retrieve the body of a result.
    /// The result instance must implement the <see cref="IHasBody" /> interface,
    /// or otherwise an <see cref="InvalidOperationException" /> will be thrown.
    /// </summary>
    /// <param name="result">The result whose body should be retrieved.</param>
    /// <exception cref="InvalidOperationException">
    /// Throw when <paramref name="result" /> cannot be cast to <see cref="IHasBody" />
    /// and thus no body retrieval is possible.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="result" /> is null.</exception>
    public static object? GetBody(this IResult result)
    {
        result.MustNotBeNull();

        if (result is IHasBody response)
            return response.GetValue();

        throw new InvalidOperationException($"The result instance {result} cannot be cast to interface IHasBody");
    }

    /// <summary>
    /// Tries to retrieve the body of a result.
    /// The result instance must implement the <see cref="IHasBody{T}" /> interface,
    /// or otherwise an <see cref="InvalidOperationException" /> will be thrown.
    /// </summary>
    /// <param name="result">The result whose body should be retrieved.</param>
    /// <exception cref="InvalidOperationException">
    /// Throw when <paramref name="result" /> cannot be cast to <see cref="IHasBody" />
    /// and thus no body retrieval is possible.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="result" /> is null.</exception>
    public static T? GetBody<T>(this IResult result)
    {
        result.MustNotBeNull();

        if (result is IHasBody<T> response)
            return response.GetValue();

        throw new InvalidOperationException($"The result instance {result} cannot be cast to interface IHasBody<{typeof(T)}>");
    }

    /// <summary>
    /// Tries to retrieve the "Location" header URL.
    /// The result instance must implement the <see cref="IHasLocationUrl" /> interface,
    /// or otherwise an <see cref="InvalidOperationException" /> will be thrown.
    /// </summary>
    /// <param name="result">The result whose "Location" header URL should be retrieved.</param>
    /// <exception cref="InvalidOperationException">
    /// Throw when <paramref name="result" /> cannot be cast to <see cref="IHasLocationUrl" />
    /// and thus no body retrieval is possible.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="result" /> is null.</exception>
    public static string? GetLocationUrl(this IResult result)
    {
        result.MustNotBeNull();

        if (result is IHasLocationUrl response)
            return response.Url;

        throw new InvalidOperationException($"The result instance {result} cannot be cast to interface IHasLocationUrl");
    }
}