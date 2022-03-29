using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Synnotech.AspNetCore.MinimalApis.Responses;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class TestEndpoints
{
    public static IEndpointRouteBuilder AddStatusCodeResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ok", () => Response.Ok());

        app.MapGet("/api/created", () => Response.Created());
        app.MapGet("/api/created/string", () => Response.Created(Location.Default.Url));
        app.MapGet("/api/created/uri", () => Response.Created(new Uri(Location.Default.Url)));

        app.MapGet("/api/accepted", () => Response.Accepted());
        app.MapGet("/api/accepted/string", () => Response.Accepted(Location.Default.Url));
        app.MapGet("/api/accepted/uri", () => Response.Accepted(new Uri(Location.Default.Url)));

        app.MapGet("/api/noContent", () => Response.NoContent());

        app.MapGet("/api/badRequest", () => Response.BadRequest());

        app.MapGet("/api/unauthorized", () => Response.Unauthorized());
        app.MapGet("/api/forbidden", () => Response.Forbidden());
        app.MapGet("/api/notAllowed", () => Response.NotAllowed());

        app.MapGet("/api/notFound", () => Response.NotFound());

        return app;
    }

    public static IEndpointRouteBuilder AddObjectResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ok/body", () => Response.Ok(Contact.Default));

        app.MapGet("/api/created/withBody", () => Response.Created(Contact.Default));
        app.MapGet("/api/created/withBody/string", () => Response.Created(Location.Default.Url, Contact.Default));
        app.MapGet("/api/created/withBody/uri", () => Response.Created(new Uri(Location.Default.Url), Contact.Default));

        app.MapGet("/api/accepted/withBody", () => Response.Accepted(Contact.Default));
        app.MapGet("/api/accepted/withBody/string", () => Response.Accepted(Location.Default.Url, Contact.Default));
        app.MapGet("/api/accepted/withBody/uri", () => Response.Accepted(new Uri(Location.Default.Url), Contact.Default));

        app.MapGet("/api/badRequest/string", () => Response.BadRequest(Contact.Default));

        app.MapGet("/api/conflict", () => Response.Conflict(Contact.Default));

        app.MapGet("/api/internalServerError", () => Response.InternalServerError(Contact.Default));

        app.MapGet("/api/unauthorized/contact", () => Response.Unauthorized(Contact.Default));

        app.MapGet("/api/forbidden/contact", () => Response.Forbidden(Contact.Default));

        return app;
    }

    public static IEndpointRouteBuilder AddRedirectAndNotAllowedResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/redirect/temporary", () => Response.RedirectTemporary(Location.DefaultRedirect.Url, false));
        app.MapGet("/api/redirect/permanent", () => Response.RedirectPermanent(Location.DefaultRedirect.Url, false));

        const string scheme = "Basic";
        var schemeList = new List<string> { scheme };
        var authenticationProperties = new AuthenticationProperties();

        app.MapGet("/api/notAllowed/authenticationScheme/string", () => Response.NotAllowed(scheme));
        app.MapGet("/api/notAllowed/authenticationScheme/list", () => Response.NotAllowed(schemeList));
        app.MapGet("/api/notAllowed/authenticationProperties", () => Response.NotAllowed(authenticationProperties));
        app.MapGet("/api/notAllowed/authenticationProperties/string", () => Response.NotAllowed(scheme, authenticationProperties));
        app.MapGet("/api/notAllowed/authenticationProperties/list", () => Response.NotAllowed(schemeList, authenticationProperties));

        return app;
    }

    public static IEndpointRouteBuilder AddFileResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/stream", HttpFileResponseHelper.SetupStreamResponse);
        app.MapGet("/api/bytes", HttpFileResponseHelper.SetupByteArrayResponse);
        app.MapGet("/api/physical", HttpFileResponseHelper.SetupPhysicalFileResponse);
        app.MapGet("/api/virtual", HttpFileResponseHelper.SetupVirtualFileResponse);

        return app;
    }
}