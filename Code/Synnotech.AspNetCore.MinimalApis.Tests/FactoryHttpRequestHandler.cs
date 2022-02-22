using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Synnotech.AspNetCore.MinimalApis.Responses;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class FactoryHttpRequestHandler
{
    private const string Url = "http://test.url";

    public static IEndpointRouteBuilder AddStatusCodeResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ok", () => Response.Ok());

        app.MapGet("/api/noContent", () => Response.NoContent());

        app.MapGet("/api/badRequest", () => Response.BadRequest());

        app.MapGet("/api/Unauthorized", () => Response.Unauthorized());
        app.MapGet("/api/Forbidden", () => Response.Forbidden());
        app.MapGet("/api/Forbidden/standard", () => Results.Forbid());

        app.MapGet("/api/NotFound", () => Response.NotFound());

        return app;
    }

    public static IEndpointRouteBuilder AddObjectResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ok/body", () => Response.Ok(Contact.Default));

        app.MapGet("/api/created/string", () => Response.Created(Contact.Default, Url));
        app.MapGet("/api/created/uri", () => Response.Created(Contact.Default, new Uri(Url)));

        app.MapGet("/api/accepted", () => Response.Accepted(Contact.Default));
        app.MapGet("/api/accepted/string", () => Response.Accepted(Url, Contact.Default));
        app.MapGet("/api/accepted/uri", () => Response.Accepted(new Uri(Url), Contact.Default));

        app.MapGet("/api/badRequest/string", () => Response.BadRequest(Contact.Default));

        app.MapGet("/api/conflict", () => Response.Conflict(Contact.Default));

        app.MapGet("/api/internalServerError", () => Response.InternalServerError(Contact.Default));

        return app;
    }

    public static IEndpointRouteBuilder AddRedirectAndForbiddenResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/redirect/temporary", () => Response.RedirectTemporary(Url, true));
        app.MapGet("/api/redirect/permanent", () => Response.RedirectPermanent(Url, true));

        var scheme = "Basic";
        var schemeList = new List<string> { scheme };
        var authenticationProperties = new AuthenticationProperties();

        app.MapGet("/api/forbidden/authenticationScheme/string", () => Response.Forbidden(scheme));
        app.MapGet("/api/forbidden/authenticationScheme/list", () => Response.Forbidden(schemeList));
        app.MapGet("/api/forbidden/authenticationProperties", () => Response.Forbidden(authenticationProperties));
        app.MapGet("/api/forbidden/authenticationProperties/string", () => Response.Forbidden(scheme, authenticationProperties));
        app.MapGet("/api/forbidden/authenticationProperties/list", () => Response.Forbidden(schemeList, authenticationProperties));

        return app;
    }

    public static IEndpointRouteBuilder AddFileResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/stream", SetupStreamResponse);
        app.MapGet("/api/bytes", SetupByteArrayResponse);

        return app;
    }

    private static StreamResponse SetupStreamResponse()
    {
        var (contentType, path) = ProvideContentTypeAndPath();

        var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

        return Response.Stream(fileStream, contentType);
    }

    private static ByteArrayResponse SetupByteArrayResponse()
    {
        var (contentType, path) = ProvideContentTypeAndPath();

        var bytes = File.ReadAllBytes(path);

        return Response.ByteArray(bytes, contentType);
    }

    private static (string? contentType, string path) ProvideContentTypeAndPath()
    {
        const string fileName = "test.json";
        new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

        var path = "./" + fileName;

        return (contentType, path);
    }
}

public sealed class Contact
{
    public static Contact Default { get; } = new () { Id = 42, Name = "John Doe" };
    
    // ReSharper disable UnusedAutoPropertyAccessor.Global -- The get method is called by the JSON serializer
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
