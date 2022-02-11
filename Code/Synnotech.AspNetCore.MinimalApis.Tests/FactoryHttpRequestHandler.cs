﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Synnotech.AspNetCore.MinimalApis.Responses;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class FactoryHttpRequestHandler
{
    const string value = "Test";
    const string url = "test.url";

    public static IEndpointRouteBuilder AddStatusCodeResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ok", () => Response.Ok());

        app.MapGet("/api/noContent", () => Response.NoContent());

        app.MapGet("/api/badRequest", () => Response.BadRequest());

        app.MapGet("/api/Unauthorized", () => Response.Unauthorized());
        app.MapGet("/api/Forbidden", () => Response.Forbidden());

        app.MapGet("/api/NotFound", () => Response.NotFound());

        return app;
    }

    public static IEndpointRouteBuilder AddObjectResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ok/body", () => Response.Ok<string>(value));

        app.MapGet("/api/created/string", () => Response.Created<string>(url, value));
        app.MapGet("/api/created/uri", () => Response.Created<string>(new Uri(url), value));

        app.MapGet("/api/accepted", () => Response.Accepted<string>(value));
        app.MapGet("/api/accepted/string", () => Response.Accepted<string>(url, value));
        app.MapGet("/api/accepted/uri", () => Response.Accepted<string>(new Uri(url), value));

        app.MapGet("/api/badRequest/string", () => Response.BadRequest<string>(value));

        app.MapGet("/api/conflict", () => Response.Conflict<string>(value));

        app.MapGet("/api/internalServerError", () => Response.InternalServerError<string>(value));

        return app;
    }

    public static IEndpointRouteBuilder AddRedirectAndForbiddenResponses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/redirect/temporary", () => Response.RedirectTemporary(url, true));
        app.MapGet("/api/redirect/permanent", () => Response.RedirectPermanent(url, true));

        var scheme = "TestScheme";
        var schemeList = new List<string> { scheme };
        var authenticationProperties = new AuthenticationProperties();

        app.MapGet("/api/forbidden/authenticationScheme/string", () => Response.Forbidden(scheme));
        app.MapGet("/api/forbidden/authenticationScheme/list", () => Response.Forbidden(schemeList));
        app.MapGet("/api/forbidden/authenticationProperties", () => Response.Forbidden(authenticationProperties));
        app.MapGet("/api/forbidden/authenticationProperties/string", () => Response.Forbidden(scheme, authenticationProperties));
        app.MapGet("/api/forbidden/authenticationProperties/list", () => Response.Forbidden(schemeList, authenticationProperties));

        return app;
    }

    /*
    public static IEndpointRouteBuilder AddFileResponses(this IEndpointRouteBuilder app)
    {
        var contentType = "application/json";
        app.MapGet("/api/stream", Response.Stream(new FileStream(), contentType));
        app.MapGet("/api/stream", Response.ByteArray(new ReadOnlyMemory<byte>(new byte[]), contentType));

        return app;
    } 
    */
}