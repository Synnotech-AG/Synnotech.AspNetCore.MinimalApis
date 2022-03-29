namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the abstraction of an HTTP response that
/// sets the "Location" header.
/// </summary>
public interface IHasLocationUrl
{
    /// <summary>
    /// Gets the URL for the "Location" header.
    /// </summary>
    string? Url { get; }
}