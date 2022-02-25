using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Synnotech.AspNetCore.MinimalApis.Responses;
using Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class FactoryHttpRequestHandler
{

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

        app.MapGet("/api/created/string", () => Response.Created(Contact.Default, Location.Default.Url));
        app.MapGet("/api/created/uri", () => Response.Created(Contact.Default, new Uri(Location.Default.Url)));

        app.MapGet("/api/accepted", () => Response.Accepted(Contact.Default));
        app.MapGet("/api/accepted/string", () => Response.Accepted(Location.Default.Url, Contact.Default));
        app.MapGet("/api/accepted/uri", () => Response.Accepted(new Uri(Location.Default.Url), Contact.Default));

        app.MapGet("/api/badRequest/string", () => Response.BadRequest(Contact.Default));

        app.MapGet("/api/conflict", () => Response.Conflict(Contact.Default));

        app.MapGet("/api/internalServerError", () => Response.InternalServerError(Contact.Default));

        return app;
    }

    public static IEndpointRouteBuilder AddRedirectAndForbiddenResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/redirect/temporary", () => Response.RedirectTemporary(Location.DefaultRedirect.Url, false));
        app.MapGet("/api/redirect/permanent", () => Response.RedirectPermanent(Location.DefaultRedirect.Url, false));

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
        app.MapGet("/api/stream", HttpFileResponseHelper.SetupStreamResponse);
        app.MapGet("/api/bytes", HttpFileResponseHelper.SetupByteArrayResponse);
        app.MapGet("/api/physical", HttpFileResponseHelper.SetupPhysicalFileResponse);

        return app;
    }
}