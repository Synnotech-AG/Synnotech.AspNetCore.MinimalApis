# Synnotech.AspNetCore.MinimalApis
*Contains extensions for ASP.NET Core Minimal APIs projects.*

[![Synnotech Logo](synnotech-large-logo.png)](https://www.synnotech.de/)

[![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](https://github.com/Synnotech-AG/Synnotech.AspNetCore.MinimalApis/blob/main/LICENSE)
[![NuGet](https://img.shields.io/badge/NuGet-0.1.0-blue.svg?style=for-the-badge)](https://www.nuget.org/packages/Synnotech.AspNetCore.MinimalApis/)

# How to Install

Synnotech.AspNetCore.MinimalApis is compiled against .NET 6 and is available as a [NuGet package](https://www.nuget.org/packages/Synnotech.AspNetCore.MinimalApis/):

- **Package Reference in csproj**: `<PackageReference Include="Synnotech.AspNetCore.MinimalApis" Version="0.1.0" />`
- **dotnet CLI**: `dotnet add package Synnotech.AspNetCore.MinimalApis`
- **Visual Studio Package Manager Console**: `Install-Package Synnotech.AspNetCore.MinimalApis`

# What does Synnotech.AspNetCore.MinimalApis offer you?

## Public IResult classes for easier unit testing

Unfortunately, ASP.NET Core Minimal APIs do not provide public implementations of the `IResult` interface. This means that you have a hard time evaluating whether an HTTP endpoint returned the correct result in your unit tests.

Instead of relying on the build-in results, you can use the public classes of this package. The easiest way to access them is via the static `Response` class that provides many factory methods to instantiate the different reponses:

```csharp
using Synnotech.AspNetCore.MinimalApis.Responses;

var app = WebApplication.Create();

app.MapPut("/api/contacts/new",
           async ([FromBody] UpdateContactDto dto,
                  [FromServices] Func<IUpdateContactSession> createSession) => 
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
});

app.Run();
```

In the example above, an HTTP endpoint that updates a contact is shown. First, `Response.BadRequest` is used to indicate potential errors with the DTO. When no corresponding contact can be found, `Response.NotFound` is used to return an HTTP 404 response. Finally, when the contact was updated successfully, `Response.NoContent` is used to indicate a successful HTTP 204 message.

The `Response` class has many other reponses like HTTP 200 OK, HTTP 308 Permanent Redirect, HTTP 401 Unauthorized or HTTP 403 Forbidden. Also, there are responses that return files streams and other types of content. Text is serialized to JSON (there is no option for XML currently). Simply use IntelliSense to see all your options, every response is well documented. 

If you think a response is missing, than please [create an issue](https://github.com/Synnotech-AG/Synnotech.AspNetCore.MinimalApis/issues).
