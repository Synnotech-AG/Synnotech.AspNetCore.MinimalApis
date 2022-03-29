namespace Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;

public sealed class Contact
{
    public static Contact Default { get; } = new () { Id = 42, Name = "John Doe" };

    // ReSharper disable UnusedAutoPropertyAccessor.Global -- The get method is called by the JSON serializer
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}