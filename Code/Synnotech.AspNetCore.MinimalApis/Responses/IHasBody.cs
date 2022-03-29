namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the abstraction of an HTTP response having a value that will
/// be serialized to the body of the message.
/// </summary>
public interface IHasBody
{
    /// <summary>
    /// Gets the body of the response as an object.
    /// </summary>
    object? GetValue();
}

/// <summary>
/// Represents the abstraction of an HTTP response having a value that will
/// be serialized to the body of the message.
/// </summary>
/// <typeparam name="T">The type of the value that will be serialized to the body of the response message.</typeparam>
public interface IHasBody<out T>
{
    /// <summary>
    /// Gets the body of the response.
    /// </summary>
    T? GetValue();
}