namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the abstraction of a response that has a status code.
/// </summary>
public interface IHasStatusCode
{
    /// <summary>
    /// Gets the status code of the response
    /// </summary>
    int StatusCode { get; }
}