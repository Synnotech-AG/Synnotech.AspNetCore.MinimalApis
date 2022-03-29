# Synnotech.AspNetCore.MinimalApis

*Contains extensions for ASP.NET Core Minimal APIs projects.*

[![Synnotech Logo](synnotech-large-logo.png)](https://www.synnotech.de/)

[![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](https://github.com/Synnotech-AG/Synnotech.AspNetCore.MinimalApis/blob/main/LICENSE)
[![NuGet](https://img.shields.io/badge/NuGet-0.2.0-blue.svg?style=for-the-badge)](https://www.nuget.org/packages/Synnotech.AspNetCore.MinimalApis/)

# How to Install

Synnotech.AspNetCore.MinimalApis is compiled against .NET 6 and is available as a [NuGet package](https://www.nuget.org/packages/Synnotech.AspNetCore.MinimalApis/):

- **Package Reference in csproj**: `<PackageReference Include="Synnotech.AspNetCore.MinimalApis" Version="0.2.0" />`
- **dotnet CLI**: `dotnet add package Synnotech.AspNetCore.MinimalApis`
- **Visual Studio Package Manager Console**: `Install-Package Synnotech.AspNetCore.MinimalApis`

# What does Synnotech.AspNetCore.MinimalApis offer you?

## Public IResult classes for easier unit testing

Unfortunately, ASP.NET Core Minimal APIs do not provide public implementations of the `IResult` interface. This means that you have a hard time evaluating whether an HTTP endpoint returned the correct result in your unit tests.

Instead of relying on the build-in results, you can use the public classes of this package. The easiest way to access them is via the static `Response` class that provides many factory methods to instantiate the different responses, for example:

- `Response.Ok(foundObject)`
- `Response.Created("/api/contacts/" + newContact.Id, newContact)`
- `Response.PermanentRedirect(newUrl)`
- `Response.BadRequest(errors)`
- `Response.ValidationProblem(problemDetails)`
- `Response.Unauthorized()`
- `Response.Forbidden()`
- `Response.NotAllowed()`
- `Response.NotFound()`
- `Response.File(stream)`
- `Response.File(filePath)`

A more comprehensive example looks like this:

```csharp
using Synnotech.AspNetCore.MinimalApis.Responses;

namespace MyWebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var app = WebApplication.Create(args);
        app.MapPut("/api/contacts", UpdateContactEndpoint.UpdateContact);
        app.Run();
    }
}

public static class UpdateContactEndpoint
{
    public static async Task<IResult> UpdateContact(
        [FromBody] UpdateContactDto dto,
        [FromServices] Func<IUpdateContactSession> createSession
    )
    {
        if (dto.CheckForErrors(dto, out var errors))
            return Response.BadRequest(errors);
    
        await using var session = createSession();
        var existingContact = await session.GetContactAsync(dto.ContactId);
        if (existingContact == null)
            return Response.NotFound();
        
        existingContact.UpdateFromDto(dto);
        await session.SaveChangesAsync();
        return Response.NoContent();
    }
}
```

In the example above, an HTTP endpoint that updates a contact is shown. First, `Response.BadRequest` is used to indicate potential errors with the DTO. When no corresponding contact can be found, `Response.NotFound` is used to return an HTTP 404 response. Finally, when the contact was updated successfully, `Response.NoContent` is used to indicate a successful HTTP 204 message.

In your tests, you can then directly call your endpoint and make validations on the returned `IResult` instances, all without running the full ASP.NET Core pipeline:

```csharp
namespace MyWebApiTests;

public class UpdateContactEndpointTests
{
    public UpdateContactEndpointTests(ITestOutputHelper output)
    {
        Output = output;
        Session = new UpdateContactSessionMock();
    }

    private ITestOutputHelper Output { get; }
    private UpdateContactSessionMock Session { get; }

    [Fact]
    public async Task UpdateValidContact()
    {
        Session.Contact = new Contact { Id = 42, Name = "John Doe" };
        var dto = new UpdateContactDto { Id = 42, NewName = "Jane Bro" };

        var result = await UpdateContactEndpoint.UpdateContact(dto, () => Session);

        Assert.IsType<NoContentResponse>(result);
        Assert.Equal(new Contact { Id = 42, Name = "Jane Bro" }, session.Contact);
        Session.SaveChangesMustHaveBeenCalled()
               .MustBeDisposed();
    }

    [Fact]
    public async Task InvalidDto()
    {
        var dto = new UpdateContactDto { Id = -12, NewName = "" };

        var result = await UpdateContactEndpoint.UpdateContact(dto, () => Session);

        Output.WriteLine(JsonSerializer.Serialize(result.GetBody()));
        Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());
        Session.MustNotHaveBeenOpened();
    }

    [Fact]
    public async Task NoContactFound()
    {
        Session.Contact = null;
        var dto = new UpdateContactDto { Id = 42, NewName = "Jane Bro" };

        var result = await UpdateContactEndpoint.UpdateContact(dto, () => Session);

        Assert.IsType<NotFoundResponse>(result);
        Session.SaveChangesMustNotHaveBeenCalled()
               .MustBeDisposed();
    }
}
```

In the three unit tests above, the first and the third one simply downcast the result to the expected response types (`NoContentResponse` and `NotFoundResponse`). The second test uses the extension methods `GetBody()` and `GetStatusCode()` to directly retrieve these two values from the `IResult` instance.

The `Response` class has many other responses like HTTP 200 OK, HTTP 308 Permanent Redirect, HTTP 401 Unauthorized or HTTP 403 Forbidden. Also, there are responses that return file streams and other types of content. Text is serialized to JSON (there is no option for XML currently). Simply use IntelliSense to see all your options on the `Response` class, every response is well documented.

If you think a response is missing, then please [create an issue](https://github.com/Synnotech-AG/Synnotech.AspNetCore.MinimalApis/issues).
